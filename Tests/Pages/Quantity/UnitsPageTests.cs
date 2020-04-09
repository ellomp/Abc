using System.Linq;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Infra.Quantity;
using Abc.Pages;
using Abc.Pages.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantity
{
    [TestClass]
    public class UnitsPageTests : AbstractClassTests<UnitsPage,
        CommonPage<IUnitsRepository, Unit, UnitView, UnitData>>
    {
        private class TestClass : UnitsPage
        {
            internal TestClass(IUnitsRepository r, IMeasuresRepository m, IUnitTermsRepository t, IUnitFactorsRepository f) : base(r, m, t, f) { }
        }

        private class UnitsRepository : BaseTestRepositoryForUniqueEntity<Unit, UnitData>, IUnitsRepository { }
        private class MeasuresRepository : BaseTestRepositoryForUniqueEntity<Measure, MeasureData>, IMeasuresRepository { }
        private class TermRepository : BaseTestRepositoryForPeriodEntity<UnitTerm, UnitTermData>, IUnitTermsRepository
        {
            protected override bool IsThis(UnitTerm entity, string id) => true;
            protected override string GetId(UnitTerm entity) => string.Empty;

        }

        private class FactorRepository : BaseTestRepositoryForPeriodEntity<UnitFactor, UnitFactorData>, IUnitFactorsRepository
        {
            protected override bool IsThis(UnitFactor entity, string id) => true;
            protected override string GetId(UnitFactor entity) => string.Empty;

        }

        private UnitsRepository _units;
        private MeasuresRepository _measures;
        private MeasureData _data;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            var t = new TermRepository();
            var f = new FactorRepository();
            _units = new UnitsRepository();
            _measures = new MeasuresRepository();
            _data = GetRandom.Object<MeasureData>();
            var m = new Measure(_data);
            _measures.Add(m).GetAwaiter();
            AddRandomMeasures();
            obj = new TestClass(_units, _measures, t, f);
        }

        private void AddRandomMeasures()
        {
            for (var i = 0; i < GetRandom.UInt8(3, 10); i++)
            {
                var d = GetRandom.Object<MeasureData>();
                var m = new Measure(d);
                _measures.Add(m).GetAwaiter();
            }
        }

        [TestMethod]
        public void ItemIdTest()
        {
            var item = GetRandom.Object<UnitView>();
            obj.Item = item;
            Assert.AreEqual(item.Id, obj.ItemId);
            obj.Item = null;
            Assert.AreEqual(string.Empty, obj.ItemId);
        }

        [TestMethod] public void PageTitleTest() => Assert.AreEqual("Units", obj.PageTitle);

        [TestMethod] public void PageUrlTest() => Assert.AreEqual("/Quantity/Units", obj.PageUrl);

        [TestMethod]
        public void ToObjectTest()
        {
            var view = GetRandom.Object<UnitView>();
            var o = obj.ToObject(view);
            ArePropertyValuesEqualTest(view, o.Data);
        }

        [TestMethod]
        public void ToViewTest()
        {
            var d = GetRandom.Object<UnitData>();
            var view = obj.ToView(new Unit(d));
            ArePropertyValuesEqualTest(view, d);
        }

        [TestMethod]
        public void LoadDetailsTest()
        {
            var t = GetRandom.Object<UnitView>();
            obj.LoadDetails(t);
            Assert.IsNotNull(obj.Terms);

            var f = GetRandom.Object<UnitView>();
            obj.LoadDetails(f);
            Assert.IsNotNull(obj.Factors);
        }

        [TestMethod]
        public void GetMeasureNameTest()
        {
            var name = obj.GetMeasureName(_data.Id);
            Assert.AreEqual(_data.Name, name);
        }

        [TestMethod]
        public void MeasuresTest()
        {
            var list = _measures.Get().GetAwaiter().GetResult();
            Assert.AreEqual(list.Count, obj.Measures.Count());
        }

        [TestMethod]
        public void TermsTest()
        {
            IsReadOnlyProperty(obj, nameof(obj.Terms), obj.Terms);
        }

        [TestMethod]
        public void FactorsTest()
        {
            IsReadOnlyProperty(obj, nameof(obj.Factors), obj.Factors);
        }
    }
}
