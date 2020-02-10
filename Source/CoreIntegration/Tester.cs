using System;
using System.Threading.Tasks;

namespace CoreIntegration
{
    public class Tester
    {
        private readonly ITestContextFactory _testContextFactory;

        public Tester(ITestContextFactory testContextFactory)
        {
            _testContextFactory = testContextFactory;
        }

        public async Task Test(string testName,
            Func<ITestContext,Task> method)
        {
            if (testName == null) throw new ArgumentNullException(nameof(testName));
            if (method == null) throw new ArgumentNullException(nameof(method));
            using var context = _testContextFactory.Create(testName);
            try
            {
                await method(context);
                Console.WriteLine($"Test Pass:{testName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in method:{testName}{Environment.NewLine}{e}{Environment.NewLine}");
            }
        }
    }
}