using Abc.Data.Common;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantity
{
    [TestClass]
    public class QuantityDbInitializerTests : BaseTests
    {
        private QuantityDbContext db;

        [TestInitialize]
        public void TestInitialize()
        {
            type = typeof(QuantityDbInitializer);
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            db = new QuantityDbContext(options);
        }

        [TestMethod] public void InitializeTest() { }

        public int getCount<T>(DbSet<T> dbSet)
            where T : PeriodData, new() => dbSet.CountAsync().GetAwaiter().GetResult();


        [TestMethod] public void MeasuresTest() => Assert.AreEqual(12, getCount(db.Measures)); 
        [TestMethod] public void UnitsTest() => Assert.AreEqual(125, getCount(db.Units));
        [TestMethod] public void MeasureTermsTest() => Assert.AreEqual(2, getCount(db.MeasureTerms));
        [TestMethod] public void UnitTermsTest() => Assert.AreEqual(39, getCount(db.UnitTerms));
        [TestMethod] public void UnitFactorsTest() => Assert.AreEqual(90, getCount(db.UnitFactors));
        [TestMethod] public void SystemOfUnitsTest() => Assert.AreEqual(2, getCount(db.SystemsOfUnits));

    }
}