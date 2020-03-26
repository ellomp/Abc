using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade
{
    [TestClass]
    public class IsFacadeTested : AssemblyTests
    {
        private const string Assembly = "Abc.Facade";

        protected override string Namespace(string name)
        {
            return $"{Assembly}.{name}";
        }

        [TestMethod] public void IsCommonTested() { IsAllTested(Assembly, Namespace("Common")); }
        [TestMethod] public void IsQuantityTested() { IsAllTested(Assembly, Namespace("Quantity")); }
        [TestMethod] public void IsTested() { IsAllTested(base.Namespace("Facade")); }

    }
}
