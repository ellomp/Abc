using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class UnitFactorViewFactoryTests : BaseTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(MeasureTermViewFactory);
        }

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest()
        {
            var view = GetRandom.Object<MeasureTermView>();
            var data = MeasureTermViewFactory.Create(view).Data;

            ArePropertyValuesEqualTest(view, data);

        }

        [TestMethod]
        public void CreateViewTest()
        {
            var view = GetRandom.Object<UnitFactorView>();
            var data = UnitFactorViewFactory.Create(view).Data;

            ArePropertyValuesEqualTest(view, data);

        }

    }

}