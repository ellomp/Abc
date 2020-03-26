﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages
{
    public abstract class BasePage<TRepository, TDomain, TView, TData> : PageModel
    where TRepository : ICrudMethods<TDomain>, ISorting, IFiltering, IPaging
    {
        private TRepository db;

        protected internal BasePage(TRepository r)
        {
            db = r;
        }

        [BindProperty]
        public TView Item { get; set; }
        public IList<TView> Items { get; set; }
        public abstract string ItemId { get; }

        public string PageTitle { get; set; }
        public string PageSubTitle => GetPageSubtitle();
        public string IndexUrl => GetIndexUrl();

        protected internal virtual string GetIndexUrl()
        {
            return $"{PageUrl}/Index?fixedFilter={FixedFilter}&fixedValue={FixedValue}";
        }

        public string PageUrl => GetPageUrl();

        protected internal abstract string GetPageUrl();

        protected internal virtual string GetPageSubtitle()
        {
            return string.Empty;
        }

        public string FixedValue
        {
            get => db.FixedValue;
            set => db.FixedValue = value;
        }

        public string FixedFilter
        {
            get => db.FixedFilter;
            set => db.FixedFilter = value;
        }
        public string SortOrder
        {
            get => db.SortOrder;
            set => db.SortOrder = value;
        }

        public string SearchString
        {
            get => db.SearchString;
            set => db.SearchString = value;
        }

        public int PageIndex
        {
            get => db.PageIndex;
            set => db.PageIndex = value;
        }

        public bool HasPreviousPage => db.HasPreviousPage;
        public bool HasNextPage => db.HasNextPage;


        public int TotalPages => db.TotalPages;

        protected internal async Task<bool> AddObject(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            try
            {
                if (!ModelState.IsValid) return false;
                await db.Add(ToObject(Item));
            }
            catch
            {
                return false;
            }

            return true;
        }

        protected internal abstract TDomain ToObject(TView view);

        protected internal async Task UpdateObject(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            //TODO see viga tuleb lahendada
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            await db.Update(ToObject(Item));
        }

        protected internal async Task GetObject(string id, string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            var o = await db.Get(id);
            Item = ToView(o);
        }

        protected internal abstract TView ToView(TDomain obj);

        protected internal async Task DeleteObject(string id, string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            await db.Delete(id);
        }

        public string GetSortString(Expression<Func<TData, object>> e, string page)
        {
            var name = GetMember.Name(e);
            string sortOrder;
            if (string.IsNullOrEmpty(SortOrder)) sortOrder = name;
            else if (!SortOrder.StartsWith(name)) sortOrder = name;
            else if (SortOrder.EndsWith("_desc")) sortOrder = name;
            else sortOrder = name + "_desc";

            return $"{page}?sortOrder={sortOrder}&currentFilter={SearchString}"
                   + $"&fixedFilter={FixedFilter}&fixedValue={FixedValue}"; //siin ei tohi stringi sees tühikuid olla
        }

        protected internal async Task GetList(string sortOrder, string currentFilter, string searchString,
            int? pageIndex, string fixedFilter, string fixedValue)                                                //sortOrder tuleb serverist, currentfiltri saadame kliendile, searchstring on see mille sisetame browseris
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            SortOrder = sortOrder;
            SearchString = GetSearchString(currentFilter, searchString, ref pageIndex);
            PageIndex = pageIndex ?? 1;
            Items = await GetList();
        }

        private string GetSearchString(string currentFilter, string searchString, ref int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            return searchString;
        }

        internal async Task<List<TView>> GetList()
        {
            var l = await db.Get();

            return l.Select(ToView).ToList();
        }
    }
}