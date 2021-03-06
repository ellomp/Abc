﻿using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity {

    public class
        SystemOfUnitsPage : CommonPage<ISystemOfUnitsRepository, SystemOfUnits, SystemOfUnitsView, SystemOfUnitsData
        > {

        protected internal SystemOfUnitsPage(ISystemOfUnitsRepository r) : base(r) 
        {
            PageTitle = "System Of Units";
        }

        public override string ItemId => Item.Id;

        protected internal override string getPageUrl() => "/Quantity/SystemOfUnits";

        protected internal override SystemOfUnits ToObject(SystemOfUnitsView view) => SystemOfUnitsViewFactory.Create(view);

        protected internal override SystemOfUnitsView ToView(SystemOfUnits obj) => SystemOfUnitsViewFactory.Create(obj);

    }
}


