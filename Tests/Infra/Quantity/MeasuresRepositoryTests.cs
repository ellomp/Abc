using System;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantity
{
    [TestClass]
    public class MeasuresRepositoryTests : RepositoryTests<MeasuresRepository, Measure, MeasureData>
    {
        private QuantityDbContext _db;
        private int _count;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _db = new QuantityDbContext(options);
            obj = new MeasuresRepository(_db);
            _count = GetRandom.UInt8(20, 40);

            foreach (var e in _db.Measures)//see loop teeb andmebaasi tühjaks, vajalik Count total items testclassi jaoks, sest luges väärtuseid topelt
                _db.Entry(e).State = EntityState.Deleted;

            AddItems();
        }

        private void AddItems()
        {
            for (int i = 0; i < _count; i++)
                obj.Add(new Measure(GetRandom.Object<MeasureData>())).GetAwaiter();
        }

        protected override Type GetBaseType() => typeof(UniqueEntityRepository<Measure, MeasureData>);

        protected override void GetListTest()
        {
            obj.PageIndex = GetRandom.Int32(2, obj.TotalPages-1);
            var l = obj.Get().GetAwaiter().GetResult();
            Assert.AreEqual(obj.PageSize, l.Count);
        }

        protected override string GetId(MeasureData d) => d.Id;

        protected override Measure GetObject(MeasureData d) => new Measure(d);

        protected override void SetId(MeasureData d, string id) => d.Id = id;
    }
}
