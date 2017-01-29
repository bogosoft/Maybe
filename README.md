# Maybe
A covariant implementation of the option pattern for .NET 4.5.

**Note: this is not currently an implementation of the Maybe Monad, nor should it ever be.**

The development of this project was motivated by a need (in my own work) to eliminate ambiguity in *suggested* behavior for interface members.

Consider the following:

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    TItem Lookup(TKey key);
}
```

A consumer of this interface may want to know a few things about the behavior of `ILookStuffUp.Lookup`:

* Will `Lookup` return the default value of `TItem` if the key does not exist?
* Will `Lookup` throw an exception if the key does not exist?
* If `Lookup` throws an exception, which one will it be?

We could address these questions in the comments, like ...

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    /// <summary>
    /// Look something up!
    /// </summary>
    /// <returns>
    /// The value associated with the current key, or the default value for the type if the key does not exist.
    /// </returns>
    TItem Lookup(TKey key);
}
```

In the above, the consumer can be *reasonably* assured that an exception won't be thrown.

But consider:

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    /// <summary>
    /// Look something up!
o    /// </summary>
    /// <returns>
    /// The value associated with the current key.
    /// </returns>
    /// <exception cref="System.InvalidOperationException"/>
    /// Thrown in the event that the given key does not exist.
    /// </exception>
    TItem Lookup(TKey key);
}
```

Here, the opposite approach was taken, and now the consumer can be *reasonably* assured that an exception *will* be thrown if the key does not exist.

There's still one major problem: no matter what the contract says about how things *should* happen, **its up to the implementor to ensure that their implemented behavior matches the intended behavior**.

Even if we did the following:

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    /// <summary>    /// Look something up!
    /// </summary>
    /// <returns>
    /// The value associated with the current key. Implementations SHOULD return the default value for
    /// the type in the event that the given key does not exist. Please see RFC 2119 for a conforming
    /// definition of the word, 'should'.
    /// </returns>
    TItem Lookup(TKey key);
}
```

Here we've been (painfully) descriptive of our intent, even to the point of leaking a desperate plea into the source. But our original problem still exists, and the ultimate behavior of the `Lookup` method is still up for grabs.

But how about this?

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    /// <summary>
    /// Look something up!
    /// </summary>
    /// <returns>
    /// The value associated with the current key. The behavior of this method is undefined when the given key does not exist.
    /// </returns>
    TItem Lookup(TKey key);
}
```

Its certainly to the point, but its only promise is to make no promises about behavior, so ultimately, it's not very helpful to a consumer of the interface.

Given that there are no compiler checks that throw errors for code that does not conform to a design philosophy, the only way to guarantee behavior to a consumer while simultaneously guaranteeing conformance from an implementor is to use the compiler itself.

Now consider:

```csharp
public interface ILookStuffUp<TKey, TItem>
{
    /// <summary>
    /// Look something up!
    /// </summary>
    /// <returns>
    /// A possible value of the item type.
    /// </returns>
    MayBe<TItem> Lookup(TKey key);
}
```

`MayBe<TItem>` is a struct, so it can't be null. Implementors have to return it, or no compilation. The type itself is clearly defined and well-documented, so there is no ambiguity concerning the behavior of the method. Consumers will *always get a possible value back*.

Using the above code has achieved two things with regards to *designing interfaces*:

* Consumers can program to an interface (as they should) and be (almost) absolutely sure that any underlying implementation will never have any strange behavior concerning method calls.
* Implementors do not have to wrestle with the, 'Should I return null, or just throw an exception?' question.

Now, an implementor can certainly throw an exception related to non-existent keys even in this scenario, despite never needing to do so. Unfortunately, no amount of defensive design will ever guard against poor implementation coding.

Well that's it. The above was intended to outline the purpose of this package as it related to *interface design*. Hopefully, it did a decent enough job.

Yes, I'm aware, there are already (non-monad) implementations of this in the wild. However, they did not meet all of the requirements that I had, so this was built from scratch.

**Remember, this is not, and is never intended to be, an implementation of the Maybe Monad.**
