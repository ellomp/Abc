using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra
{
    [TestClass]
    public class BaseRepositoryTests : AbstractClassTests<BaseRepository<Measure, MeasureData>, object>
    {
        private MeasureData data; //genereerin objeki

        private class TestClass : BaseRepository<Measure, MeasureData>
        {
            public TestClass(DbContext c, DbSet<MeasureData> s) : base(c, s)
            {
            }


            protected internal override Measure toDomainObject(MeasureData d) => new Measure(d);

            protected override async Task<MeasureData> getData(string id)
            {
                return await dbSet.FirstOrDefaultAsync(m => m.Id == id); //otsi mulle see asi mille id'd on == (võrdsed) 
            }

            protected override string GetId(Measure entity) => entity?.Data?.Id;
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDbContext(options);
            obj = new TestClass(c, c.Measures);
            data = GetRandom.Object<MeasureData>();
        }

        [TestMethod]
        public void GetTest()
        {
            var count = GetRandom.UInt8(15, 30);
            var countBefore = obj.Get().GetAwaiter().GetResult().Count;
            for (var i = 0; i < count; i++)
            {
                data = GetRandom.Object<MeasureData>();
                AddTest();
            }
            Assert.AreEqual(count+countBefore, obj.Get().GetAwaiter().GetResult().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            AddTest();
        }

        [TestMethod]
        public void DeleteTest()
        {
            AddTest();
            var expected = obj.Get(data.Id).GetAwaiter().GetResult(); //kuna on async siis tuleb panna getawaiter. //lähen obj id järgi seda otsima
            ArePropertyValuesEqualTest(data, expected.Data);
            obj.Delete(data.Id).GetAwaiter(); //kustutan ära
            expected = obj.Get(data.Id).GetAwaiter().GetResult(); //nüüd lisan ta uuesti db
            Assert.IsNull(expected.Data); //ja nüüd peab  see null olema
        }

        [TestMethod]
        public void AddTest()
        {
            var expected = obj.Get(data.Id).GetAwaiter().GetResult(); //kuna on async siis tuleb panna getawaiter. //lähen obj id järgi seda otsima
            Assert.IsNull(expected.Data); //esimesena peab see null olema
            obj.Add(new Measure(data)).GetAwaiter(); //peale seda kui olen o lisanud, peab ta õige asja andma (?)
            expected = obj.Get(data.Id).GetAwaiter().GetResult();
            ArePropertyValuesEqualTest(data, expected.Data);

        }

        [TestMethod]
        public void UpdateTest()
        {
            AddTest(); 
            var newData = GetRandom.Object<MeasureData>();
            newData.Id = data.Id;
            obj.Update(new Measure(newData)).GetAwaiter(); //lisan andmebaasi
            var expected = obj.Get(data.Id).GetAwaiter().GetResult();   
            ArePropertyValuesEqualTest(newData, expected.Data);
        }
    }
}
