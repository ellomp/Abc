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

        [TestInitialize]
        public override void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _db = new QuantityDbContext(options);
            dbSet = ((QuantityDbContext)_db).Measures;
            obj = new MeasuresRepository((QuantityDbContext)_db);
           
            base.TestInitialize();
              
        }

        protected override Type GetBaseType() => typeof(UniqueEntityRepository<Measure, MeasureData>);

        protected override string GetId(MeasureData d) => d.Id;

        protected override Measure GetObject(MeasureData d) => new Measure(d);

        protected override void SetId(MeasureData d, string id) => d.Id = id;
    }
}
