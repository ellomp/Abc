using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests 
{
    public abstract class SealedClassTest<TClass, TBaseClass> where TClass: new() //siin �tlen ka �ra, et TClassil PEAB olema t�hjade argumentidega constructor
    {
        protected TClass obj;
        protected Type type;

        // kui pean testimisel mitu x kaustama mingit obj v t��pi, siis teen constructoirsse nt obj ja need luuakse igakord kui testid k�ivitatakse.ja testinitializei ei ole vaja testides v�lja kutsuda.
        [TestInitialize]
        public virtual void TestInitialize() 
        {
            obj = new TClass();
            type = obj.GetType();
        }

        [TestMethod]
        public void CanCreateTest()
        {
            Assert.IsNotNull(obj); 
        }

        [TestMethod]
        public void InInheritedTest() 
        {
            Assert.AreEqual(typeof(TBaseClass), type.BaseType);
        }
        [TestMethod]
        public void IsSealedTest() 
        {
            Assert.IsTrue(type.IsSealed);
        }
    }
}