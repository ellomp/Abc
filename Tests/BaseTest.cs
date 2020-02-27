using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class BaseTest<TClass, TBaseClass> where TClass: new() //siin �tlen ka �ra, et TClassil PEAB olema t�hjade argumentidega constructor
    {
        [TestMethod]
        public void CanCreateTest() //tavaliselt esimene test on alati cancreate
        {
            Assert.IsNotNull(new TClass()); //pean saama teda luua
        }

        [TestMethod]
        public void InInheritedTest() //peab olema p�ritav
        {
            Assert.AreEqual(typeof(TBaseClass), new TClass().GetType().BaseType);
        }
    }
}