﻿using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.UnitFactors
{

    public class EditModel : UnitFactorsPage
    {
        public EditModel(IUnitFactorsRepository r, IUnitsRepository u, ISystemOfUnitsRepository s) : base(r, u, s) { }

        public async Task<IActionResult> OnGetAsync(string id, string fixedFilter, string fixedValue)
        {
            await GetObject(id, fixedFilter, fixedValue);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string fixedFilter, string fixedValue)
        {
            await UpdateObject(fixedFilter, fixedValue);

            return Redirect(IndexUrl);
        }
    }
}