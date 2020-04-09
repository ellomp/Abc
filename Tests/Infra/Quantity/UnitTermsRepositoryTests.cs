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
    public class UnitTermsRepositoryTests : RepositoryTests<UnitTermsRepository, UnitTerm, UnitTermData>
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            db = new QuantityDbContext(options);
            dbSet = ((QuantityDbContext)db).UnitTerms;
            obj = new UnitTermsRepository((QuantityDbContext)db);

            base.TestInitialize();

        }

        protected override Type GetBaseType() => typeof(PaginatedRepository<UnitTerm, UnitTermData>);

        protected override string GetId(UnitTermData d) => $"{d.MasterId}.{d.TermId}";

        protected override UnitTerm GetObject(UnitTermData d) => new UnitTerm(d);

        protected override void SetId(UnitTermData d, string id)
        {
            var masterId = GetString.Head(id);
            var termId = GetString.Tail(id);
            d.MasterId = masterId;
            d.TermId = termId;
        }
    }
}
