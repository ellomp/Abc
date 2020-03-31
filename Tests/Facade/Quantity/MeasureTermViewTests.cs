﻿using Abc.Facade.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class MeasureTermViewTests :  SealedClassTests<MeasureTermView, CommonTermView>
    {
        [TestMethod] public void MasterId() => IsNullableProperty(() => obj.MasterId, x => obj.MasterId = x);
    }
}
