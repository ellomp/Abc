using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages {

    [TestClass]
    public class CrudPageTests : AbstractPageTests<CrudPage<IMeasuresRepository, Measure, MeasureView, MeasureData>,
        BasePage<IMeasuresRepository, Measure, MeasureView, MeasureData>> {

        private string _fixedFilter;
        private string _fixedValue;

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = new TestClass(db);
            _fixedFilter = GetRandom.String();
            _fixedValue = GetRandom.String();
            Assert.AreEqual(null, obj.FixedValue);
            Assert.AreEqual(null, obj.FixedFilter);
        }

        [TestMethod] public void ItemTest() {
            IsProperty(() => obj.Item, x => obj.Item = x);
        }

        [TestMethod] public void AddObjectTest() {
            var idx = db.list.Count;
            obj.Item = GetRandom.Object<MeasureView>();
            obj.AddObject(_fixedFilter, _fixedValue).GetAwaiter();
            Assert.AreEqual(_fixedFilter, obj.FixedFilter);
            Assert.AreEqual(_fixedValue, obj.FixedValue);
            ArePropertyValuesEqualTest(obj.Item, db.list[idx].Data);
        }

        [TestMethod] public void UpdateObjectTest() {
            GetObjectTest();
            var idx = GetRandom.Int32(0, db.list.Count);
            var itemId = db.list[idx].Data.Id;
            obj.Item = GetRandom.Object<MeasureView>();
            obj.Item.Id = itemId;
            obj.UpdateObject(_fixedFilter, _fixedValue).GetAwaiter();
            ArePropertyValuesEqualTest(db.list[^1].Data, obj.Item);
        }

        [TestMethod] public void GetObjectTest() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);
            for (var i = 0; i < count; i++) AddObjectTest();
            var item = db.list[idx];
            obj.GetObject(item.Data.Id, _fixedFilter, _fixedValue).GetAwaiter();
            Assert.AreEqual(count, db.list.Count);
            ArePropertyValuesEqualTest(item.Data, obj.Item);
        }

        [TestMethod] public void DeleteObjectTest() {
            AddObjectTest();
            obj.DeleteObject(obj.Item.Id, _fixedFilter, _fixedValue).GetAwaiter();
            Assert.AreEqual(_fixedFilter, obj.FixedFilter);
            Assert.AreEqual(_fixedValue, obj.FixedValue);
            Assert.AreEqual(0, db.list.Count);
        }

        [TestMethod] public void ToViewTest() {
            var d = GetRandom.Object<MeasureData>();
            var v = obj.ToView(new Measure(d));
            ArePropertyValuesEqualTest(d, v);
        }

        [TestMethod] public void ToObjectTest() {
            var v = GetRandom.Object<MeasureView>();
            var o = obj.ToObject(v);
            ArePropertyValuesEqualTest(v, o.Data);
        }

    }

}
