using System.Collections.Generic;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantity
{

    public class UnitFactorsPage : CommonPage<IUnitFactorsRepository, UnitFactor, UnitFactorView, UnitFactorData>
    {

        protected internal UnitFactorsPage(IUnitFactorsRepository r, IUnitsRepository u, ISystemOfUnitsRepository s) : base(r)
        {
            PageTitle = "Unit Factors";
            Units = createSelectList<Unit, UnitData>(u);
            SystemsOfUnits = createSelectList<SystemOfUnits, SystemOfUnitsData>(s);
        }
        public IEnumerable<SelectListItem> Units { get; }
        public IEnumerable<SelectListItem> SystemsOfUnits { get; }

        public override string ItemId => Item is null ? string.Empty : Item.GetId();

        protected internal override string getPageUrl() => "/Quantity/UnitFactors";

        protected internal override UnitFactor ToObject(UnitFactorView view) => UnitFactorViewFactory.Create(view);

        protected internal override UnitFactorView ToView(UnitFactor obj) => UnitFactorViewFactory.Create(obj);

        public string GetUnitName(string unitId)
        {
            foreach (var m in Units)
                if (m.Value == unitId)
                    return m.Text;

            return "Unspecified";
        }

        public string GetSystemOfUnitName(string systemOfUnitId)
        {
            foreach (var m in SystemsOfUnits)
                if (m.Value == systemOfUnitId)
                    return m.Text;

            return "Unspecified";
        }

        protected internal override string getPageSubTitle()
        {
            return FixedValue is null
                ? base.getPageSubTitle()
                : $"For {GetUnitName(FixedValue)}";
        }
    }
}