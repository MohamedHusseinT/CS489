using System;
using System.Threading.Tasks;

namespace ADSDentalSurgeriesWebAPI.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DetailedTestRunner.RunAllTests();
        }
    }
}
