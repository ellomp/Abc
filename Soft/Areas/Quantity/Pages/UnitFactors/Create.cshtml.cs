using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.UnitFactors
{

    public class CreateModel : UnitFactorsPage
    {
        public CreateModel(IUnitFactorsRepository r, IUnitsRepository u, ISystemOfUnitsRepository s) : base(r, u, s) { }

        public IActionResult OnGet(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string fixedFilter, string fixedValue) 
            => !await AddObject(fixedFilter, fixedValue) ? (IActionResult) Page() : Redirect(IndexUrl);

    }
}