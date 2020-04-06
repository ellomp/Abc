 using System;
 using Abc.Aids;
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
        public void IsInheritedTest()
        {
            Assert.AreEqual(GetBaseClass(), type.BaseType);
        }

        protected virtual Type GetBaseClass()
        {
            return typeof(TBaseClass);
        }

        protected static void IsNullableProperty<T>(Func<T> get, Action<T> set)
        {
            IsProperty(get, set);
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
        protected static void IsNullableProperty(object o, string name, Type type)
        {
            var property = o.GetType().GetProperty(name); //saan tüübi
            Assert.IsNotNull(property);
            Assert.AreEqual(type, property.PropertyType); //kontorllin kas prop tüüp on õige
            Assert.IsTrue(property.CanWrite);
            Assert.IsTrue(property.CanRead);
            property.SetValue(o, null);
            var actual = property.GetValue(o); //loen prop väärtuse sellest objektist
            Assert.AreEqual(null, actual);
        }
        protected static void IsPropertyTypeOf(object o, string name, Type expected)
        {
            var property = o.GetType().GetProperty(name);
            Assert.IsNotNull(property);
            Assert.AreEqual(expected, property.PropertyType);
        }

    }

}