﻿using System;
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
    public class MeasureTermsRepositoryTests : RepositoryTests<MeasureTermsRepository, MeasureTerm, MeasureTermData>
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _db = new QuantityDbContext(options);
            dbSet = ((QuantityDbContext)_db).MeasureTerms;
            obj = new MeasureTermsRepository((QuantityDbContext)_db);

            base.TestInitialize();

        }

        protected override Type GetBaseType() => typeof(PaginatedRepository<MeasureTerm, MeasureTermData>);

        protected override string GetId(MeasureTermData d) => $"{d.MasterId}.{d.TermId}";

        protected override MeasureTerm GetObject(MeasureTermData d) => new MeasureTerm(d);

        protected override void SetId(MeasureTermData d, string id)
        {
            var masterId = GetString.Head(id);
            var termId = GetString.Tail(id);
            d.MasterId = masterId;
            d.TermId = termId;
        }
    }
}
