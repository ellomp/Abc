using System;
using Abc.Aids;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra
{
    [TestClass]
    public abstract class RepositoryTests<TRepository, TObject, TData> 
        : BaseTests
        where TRepository : IRepository<TObject> // see osa mis tuleb peale :, tuleb sellest millest need "objektid pärivad"
        where TObject : Entity<TData>
        where TData : PeriodData, new() //kui see new pole, siis me seda entityt kätte ei saaks
    {
        private TData data;
        protected TRepository obj;
        protected DbContext db;
        protected int _count;
        protected DbSet<TData> dbSet;

        public virtual void TestInitialize()
        {
            type = typeof(TRepository);
            data = GetRandom.Object<TData>();
            _count = GetRandom.UInt8(20, 40);
            CleanDbSet();
            AddItems();
        }
        protected void GetListTest()
        {
            obj.PageIndex = GetRandom.Int32(2, obj.TotalPages - 1);
            var l = obj.Get().GetAwaiter().GetResult();
            Assert.AreEqual(obj.PageSize, l.Count);
        } 

        [TestCleanup]
        public void TestCleanup() //kui ta midagi teeb, siis laseb ära kustutdaa
        {
            CleanDbSet();
        }

        protected void CleanDbSet()
        {
            foreach (var e in dbSet) //see loop teeb andmebaasi tühjaks, vajalik Count total items testclassi jaoks, sest luges väärtuseid topelt
                db.Entry(e).State = EntityState.Deleted;
            db.SaveChanges();
        }

        protected void AddItems()
        {
            for (int i = 0; i < _count; i++)
                obj.Add(GetObject(GetRandom.Object<TData>())).GetAwaiter();
        }

        [TestMethod] public void IsSealedTest() => Assert.IsTrue(type.IsSealed);
        [TestMethod] public void IsInheritedTest()
        {
            Assert.AreEqual(GetBaseType().Name, type?.BaseType?.Name);
        }

        protected abstract Type GetBaseType();

        [TestMethod] public void GetTest() => GetListTest();

        [TestMethod] public void GetByIdTest() => AddTest();

        [TestMethod]
        public void DeleteTest()
        {
            AddTest();
            var id = GetId(data);
            var expected = obj.Get(id).GetAwaiter().GetResult();
            ArePropertyValuesEqualTest(data, expected.Data);
            obj.Delete(id).GetAwaiter();
            expected = obj.Get(id).GetAwaiter().GetResult();
            Assert.IsNull(expected.Data);
        }

        protected abstract string GetId(TData d);

        [TestMethod]
        public void AddTest()
        {
            var id = GetId(data);
            var expected = obj.Get(id).GetAwaiter().GetResult();
            Assert.IsNull(expected.Data);
            obj.Add(GetObject(data)).GetAwaiter();
            expected = obj.Get(id).GetAwaiter().GetResult();
            ArePropertyValuesEqualTest(data, expected.Data);
        }

        protected abstract TObject GetObject(TData d);

        [TestMethod]
        public void UpdateTest()
        {
            AddTest();
            var id = GetId(data);
            var newData = GetRandom.Object<TData>();
            SetId(newData, id);
            obj.Update(GetObject(newData)).GetAwaiter();
            var expected = obj.Get(id).GetAwaiter().GetResult();
            ArePropertyValuesEqualTest(newData, expected.Data);
        }
        
        protected abstract void SetId(TData d, string id);
    }
}