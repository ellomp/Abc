using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common
{
    [TestClass]
    public class NamedViewTests : AbstractClassTests<NamedView, UniqueEntityView>
    {
        private class TestClass : NamedView
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }
        [TestMethod]
        public void NameTest()
        {
            IsNullableProperty(() => obj.Name, x => obj.Name = x);
        }
        [TestMethod]
        public void CodeTest()
        {
            IsNullableProperty(() => obj.Code, x => obj.Code = x);
        }
    }
}