﻿using Abc.Facade.Common;
using Abc.Facade.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class CommonTermViewTests : AbstractClassTests<CommonTermView, PeriodView>
    {

        private class TestClass : CommonTermView { }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }

        [TestMethod] public void PowerTest() => IsProperty(() => obj.Power, x => obj.Power = x);

        [TestMethod] public void TermIdTest() => IsNullableProperty(() => obj.TermId, x => obj.TermId = x);

        [TestMethod] public void MasterIdTest() => IsNullableProperty(() => obj.MasterId, x => obj.MasterId = x);
    }

}