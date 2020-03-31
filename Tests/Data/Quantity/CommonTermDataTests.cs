using Abc.Data.Common;
using Abc.Data.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantity
{
    [TestClass]
    public class CommonTermDataTests : AbstractClassTests<CommonTermData, PeriodData>
    {

        private class TestClass : CommonTermData { }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }

        [TestMethod] public void TermIdTest() => IsNullableProperty(() => obj.TermId, x => obj.TermId = x);

        [TestMethod] public void MasterIdTest() => IsNullableProperty(() => obj.MasterId, x => obj.MasterId = x);

        [TestMethod] public void PowerTest() => IsProperty(() => obj.Power, x => obj.Power = x);

    }

}
