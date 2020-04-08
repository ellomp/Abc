using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Tests
{
    internal abstract class BaseTestRepositoryForPeriodEntity<TObj, TData>
        where TObj : Entity<TData>
        where TData : PeriodData, new()
    {
        internal readonly List<TObj> list;
        public BaseTestRepositoryForPeriodEntity()
        {
            list = new List<TObj>();
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public string FixedFilter { get; set; }
        public string FixedValue { get; set; }

        public async Task<List<TObj>> Get()
        {
            await Task.CompletedTask; //feigime midagi
            return list;
        }

        public async Task<TObj> Get(string id)
        {
            await Task.CompletedTask; //feigime midagi
            return list.Find(x => IsThis(x, id));
        }

        public async Task Delete(string id)
        {
            await Task.CompletedTask; //feigime midagi
            var obj = list.Find(x => IsThis(x, id));
            list.Remove(obj);
        }

        protected abstract bool IsThis(TObj entity, string id);

        public async Task Add(TObj obj)
        {
            await Task.CompletedTask; //feigime midagi
            list.Add(obj);
        }

        public async Task Update(TObj obj)
        {
            await Delete(GetId(obj));
            list.Add(obj);
        }

        protected abstract string GetId(TObj entity);
    }
}