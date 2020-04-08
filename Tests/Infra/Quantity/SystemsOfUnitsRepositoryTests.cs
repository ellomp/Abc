using System;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantity
{
    [TestClass]
    public class SystemsOfUnitsRepositoryTests : RepositoryTests<SystemOfUnitsRepository, SystemOfUnits, SystemOfUnitsData>
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _db = new QuantityDbContext(options);
            dbSet = ((QuantityDbContext)_db).SystemsOfUnits;
            obj = new SystemOfUnitsRepository((QuantityDbContext)_db);

            base.TestInitialize();

        }

        protected override Type GetBaseType() => typeof(UniqueEntityRepository<SystemOfUnits, SystemOfUnitsData>);

        protected override string GetId(SystemOfUnitsData d) => d.Id;

        protected override SystemOfUnits GetObject(SystemOfUnitsData d) => new SystemOfUnits(d);

        protected override void SetId(SystemOfUnitsData d, string id) => d.Id = id;
    }
}
