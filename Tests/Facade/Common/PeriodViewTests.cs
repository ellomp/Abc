using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common
{
    [TestClass]
    public class PeriodViewTests : AbstractClassTests<PeriodView, object>     
    {
        private class TestClass : PeriodView
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }
        [TestMethod]
        public void ValidFromTest()
        {
            //mul on funkt ja see f annab mulle kttte valid fromi ja ss on mu funt 
            //et kui ma selle argumendi x annan siis se omastatakse validfrom väärtusele.
            IsNullableProperty(() => obj.ValidFrom, x => obj.ValidFrom = x);
        }

        [TestMethod]
        public void ValidToTest()
        {
            IsNullableProperty(() => obj.ValidTo, x => obj.ValidTo = x);
        }
    }
}