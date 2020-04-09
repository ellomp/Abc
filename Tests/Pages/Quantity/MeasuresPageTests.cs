﻿using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages;
using Abc.Pages.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantity
{
    [TestClass]
    public class MeasuresPageTests : AbstractClassTests<MeasuresPage,
        CommonPage<IMeasuresRepository, Measure, MeasureView, MeasureData>>
    {
        private class TestClass : MeasuresPage
        {
            internal TestClass(IMeasuresRepository r, IMeasureTermsRepository t) : base(r, t) { }
        }

        private class TestRepository : BaseTestRepositoryForUniqueEntity<Measure, MeasureData>, IMeasuresRepository { }
        private class TermRepository : BaseTestRepositoryForPeriodEntity<MeasureTerm, MeasureTermData>, IMeasureTermsRepository 
        {
            protected override bool IsThis(MeasureTerm entity, string id) => true;
            protected override string GetId(MeasureTerm entity) => string.Empty;
            
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            var r = new TestRepository();
            var t = new TermRepository();
            obj = new TestClass(r, t);
        }

        [TestMethod]
        public void ItemIdTest()
        {
            var item = GetRandom.Object<MeasureView>();
            obj.Item = item;
            Assert.AreEqual(item.Id, obj.ItemId);
            obj.Item = null;
            Assert.AreEqual(string.Empty, obj.ItemId);
        }

        [TestMethod] public void PageTitleTest() => Assert.AreEqual("Measures", obj.PageTitle);

        [TestMethod] public void PageUrlTest() => Assert.AreEqual("/Quantity/Measures", obj.PageUrl);

        [TestMethod] public void ToObjectTest()
        {
            var view = GetRandom.Object<MeasureView>();
            var o = obj.ToObject(view);
            ArePropertyValuesEqualTest(view, o.Data);
        }
        [TestMethod] public void ToViewTest()
        {
            var data = GetRandom.Object<MeasureData>();
            var view = obj.ToView(new Measure(data));
            ArePropertyValuesEqualTest(view, data);
        }

        [TestMethod] public void LoadDetailsTest()
        {
            var v = GetRandom.Object<MeasureView>();
            obj.LoadDetails(v);
            Assert.IsNotNull(obj.Terms);
        }

        [TestMethod] public void TermsTest()
        {
            IsReadOnlyProperty(obj, nameof(obj.Terms), obj.Terms);
        }
    }

   
}
