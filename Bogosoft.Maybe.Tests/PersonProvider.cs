namespace Bogosoft.Maybe.Tests
{
    class PersonProvider
    {
        internal Person Person = null;

        internal IMayBe<Person> Get()
        {
            return Person.Maybe();
        }
    }
}