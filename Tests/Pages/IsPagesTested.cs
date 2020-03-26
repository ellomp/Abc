using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages
{
    [TestClass]
    public class IsPagesTested: AssemblyTests
    {
        private const string Assembly = "Abc.Pages";

        protected override string Namespace(string name)
        {
            return $"{Assembly}.{name}";
        }

        [TestMethod] public void IsExtensionsTested() { IsAllTested(Assembly, Namespace("Extensions")); }
        [TestMethod] public void IsQuantityTested() { IsAllTested(Assembly, Namespace("Quantity")); }
        [TestMethod] public void IsTested() { IsAllTested(base.Namespace("Pages")); }

    }
}
