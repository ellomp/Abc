using System;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra
{
    [TestClass]
    public class PaginatedRepositoryTests : AbstractClassTests<PaginatedRepository<Measure, MeasureData>, FilteredRepository<Measure, MeasureData>>
    {
        private class testClass : PaginatedRepository<Measure, MeasureData>
        {

            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s) { }

            protected internal override Measure toDomainObject(MeasureData d) => new Measure(d);

            protected override async Task<MeasureData> getData(string id) => await dbSet.FirstOrDefaultAsync(m => m.Id == id);

            protected override string GetId(Measure entity) => entity?.Data?.Id;

        }

        private byte count;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            var c = new QuantityDbContext(options);
            obj = new testClass(c, c.Measures);
            count = GetRandom.UInt8(10, 30);

            foreach (var p in c.Measures) //see loop teeb andmebaasi tühjaks, vajalik Count total items testclassi jaoks, sest luges väärtuseid topelt
            {
                c.Entry(p).State = EntityState.Deleted;
            }

            AddItems(); //viskas errori kui parameetriks oli count, bt all AddItmens method
        }

        [TestMethod]
        public void PageIndexTest()
        {
            IsProperty(()=>obj.PageIndex, x=> obj.PageIndex=x);
        }
        [TestMethod]
        public void TotalPagesTest()
        {
            var expected = (int) Math.Ceiling(count /(double) obj.PageSize); // (double) teisendab page sizei komaga arvuks
            var totalPagesCount = obj.TotalPages;
            Assert.AreEqual(expected, totalPagesCount);
        }
        [TestMethod]
        public void HasNextPageTest()
        {
            void TestNextPage(int pageIndex, bool expected)
            {
                obj.PageIndex = pageIndex;
                var actual = obj.HasNextPage;
                Assert.AreEqual(expected, actual);
            }

            TestNextPage(0, true); //kui lk nr on 0, siis on järgmine lk
            TestNextPage(GetRandom.Int32(1, obj.TotalPages-1), true); //ükskõik milline lk nr 0 ja totalpage vahel peab tulema true
            TestNextPage(obj.TotalPages, false); //kui lk arv on totalpage, siis ei ole järgmist lk

        }
        [TestMethod]
        public void HasPreviousPageTest()
        {
            void TestPreviousPage(int pageIndex, bool expected)
            {
                obj.PageIndex = pageIndex;
                var actual = obj.HasPreviousPage;
                Assert.AreEqual(expected, actual);
            }

            TestPreviousPage(0, false); //kui lk nr on 0, siis ei ole eelmist lk
            TestPreviousPage(GetRandom.Int32(1, obj.TotalPages-1), true); //ükskõik milline lk nr 0 ja totalpage vahel peab tulema true
            TestPreviousPage(obj.TotalPages-1, true);
        } 
        [TestMethod]
        public void PageSizeTest()
        {
            Assert.AreEqual(5, obj.PageSize);
            IsProperty(() => obj.PageSize, x => obj.PageSize = x);
        }
        [TestMethod]
        public void GetTotalPagesTest()
        {
            var expected = (int)Math.Ceiling(count / (double)obj.PageSize); // (double) teisendab page sizei komaga arvuks
            var totalPagesCount = obj.GetTotalPages(obj.PageSize);
            Assert.AreEqual(expected, totalPagesCount);
        }
        [TestMethod]
        public void CountTotalPagesTest()
        {
            var expected = (int)Math.Ceiling(count / (double)obj.PageSize); // (double) teisendab page sizei komaga arvuks
            var totalPagesCount = obj.CountTotalPages(count, obj.PageSize);
            Assert.AreEqual(expected, totalPagesCount);
        }
        [TestMethod]
        public void GetItemsCountTest()
        {
            var itemsCount = obj.GetItemsCount();
            Assert.AreEqual(count, itemsCount);
        }

        private void AddItems() //kui error tuli, siis siin parameetrit polnud
        {
            for (int i = 0; i < count; i++)
            {
                obj.Add(new Measure(GetRandom.Object<MeasureData>())).GetAwaiter();
            }
        }

        [TestMethod]
        public void CreateSqlQueryTest()
        {
            var o = obj.CreateSqlQuery();
            Assert.IsNotNull(o);
        }
        [TestMethod]
        public void AddSkipAndTakeTest()
        {
            var sql = obj.CreateSqlQuery(); //siit saan originaal sql query
            var o = obj.AddSkipAndTake(sql);
            Assert.IsNotNull(o);
        }
    }
}
