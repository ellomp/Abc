using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data
{
    [TestClass]
    public class IsDataTested : AssemblyTests
    {
        private const string Assembly = "Abc.Data";

        protected override string Namespace(string name)
        {
            return $"{Assembly}.{name}";
        }

        [TestMethod] public void IsCommonTested() { IsAllTested(Assembly, Namespace("Common"));}
        [TestMethod] public void IsMoneyTested() { IsAllTested(Assembly, Namespace("Money")); }
        [TestMethod] public void IsQuantityTested() { IsAllTested(Assembly, Namespace("Quantity")); }
        [TestMethod] public void IsTested() { IsAllTested(base.Namespace("Data")); }


    }
}
