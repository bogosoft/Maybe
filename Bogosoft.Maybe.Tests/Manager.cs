using System.Collections.Generic;

namespace Bogosoft.Maybe.Tests
{
    class Manager : Employee
    {
        internal IEnumerable<Employee> Reports { get; set; }
    }
}