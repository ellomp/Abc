using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests
{
    public abstract class SealedClassTest<TClass, TBaseClass> :ClassTest<TClass, TBaseClass> where TClass: new()
    //siin �tlen ka �ra, et TClassil PEAB olema t�hjade argumentidega constructor
    {
       [TestMethod]
        public void IsSealedTest()
        {
            Assert.IsTrue(type.IsSealed);
        }
    }

    //abstract klassi ei saa createda
    //abstract klasside puhul ei saa obj luua.
}