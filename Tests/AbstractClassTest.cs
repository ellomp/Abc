using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class AbstractClassTest<TClass, TBaseClass> : BaseTest<TClass, TBaseClass>
    {
        [TestMethod]
        public void IsAbstractTest()
        {
            Assert.IsTrue(type.IsAbstract);
        }
    }
}