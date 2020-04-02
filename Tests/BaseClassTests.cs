 using System;
 using Abc.Aids;
 using Abc.Tests.Infra;
 using Microsoft.VisualStudio.TestTools.UnitTesting;


 namespace Abc.Tests
{
    public abstract class BaseClassTests<TClass, TBaseClass> : BaseTests
    {
        protected TClass obj;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(TClass);
        }

        [TestMethod]
        public void InInheritedTest()
        {
            Assert.AreEqual(typeof(TBaseClass), type.BaseType);
        }

        protected static void IsNullableProperty<T>(Func<T> get, Action<T> set)
        {
            IsProperty(get, set);
            var d = (T)GetRandom.Value(typeof(T));
            set(default);
            Assert.IsNull(get());
        }

        protected static void IsProperty<T>(Func<T> get, Action<T> set)
        {
            var d = (T)GetRandom.Value(typeof(T));
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
        }

        protected static void IsReadOnlyProperty(object o, string name, object expected)
        {
            var property = o.GetType().GetProperty(name);
            Assert.IsNotNull(property);
            Assert.IsFalse(property.CanWrite);
            Assert.IsTrue(property.CanRead);
            var actual = property.GetValue(o);
            Assert.AreEqual(expected, actual);
        }
    }
}