﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class FilteredRepository<TDomain, TData> : SortedRepository<TDomain, TData>, IFiltering
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        public string SearchString { get; set; }
        public string FixedFilter { get; set; }
        public string FixedValue { get; set; }

        protected FilteredRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }

        protected internal override IQueryable<TData> CreateSqlQuery()
        {
            var query = base.CreateSqlQuery(); //võtan query
            query = AddFixedFiltering(query); //lisan filtri
            query = AddFiltering(query);

            return query; //annan tagasi
        }

        private IQueryable<TData> AddFixedFiltering(IQueryable<TData> query)
        {
            var expression = CreateFixedWhereExpression();

            return expression is null ? query : query.Where(expression);
        }

        private Expression<Func<TData, bool>> CreateFixedWhereExpression()
        {
            if (FixedFilter is null) return null;
            if (FixedValue is null) return null;

            var param = Expression.Parameter(typeof(TData), "s");
            var p = typeof(TData).GetProperty(FixedFilter);

            if (p is null) return null;
            Expression body = Expression.Property(param, p); 

            if (p.PropertyType != typeof(string))
                body = Expression.Call(body, "ToString", null);
            body = Expression.Call(body, "Contains", null, Expression.Constant(FixedValue));
            var predicate = body;

            return Expression.Lambda<Func<TData, bool>>(predicate, param);

        }

        protected internal IQueryable<TData> AddFiltering(IQueryable<TData> query)
        {
            if (string.IsNullOrEmpty(SearchString)) return query;
            var expression = CreateWhereExpression();

            return query.Where(expression);
        }

        internal Expression<Func<TData, bool>> CreateWhereExpression()
        {
            var param = Expression.Parameter(typeof(TData), "s");
            Expression predicate = null;

            foreach (var p in typeof(TData).GetProperties())
            {
                Expression body = Expression.Property(param, p);  //kui ei ole string
                if (p.PropertyType != typeof(string))
                    body = Expression.Call(body, "ToString", null); //teeme stringiks
                body = Expression.Call(body, "Contains", null, Expression.Constant(SearchString)); //rakendame meetodit contains, peab si seda mis seat seasrchstrigist tuleb
                predicate = predicate is null ? body : Expression.Or(predicate, body); //ifelse lause
            }

            return predicate is null ? null : Expression.Lambda<Func<TData, bool>>(predicate, param);
        }
    }
}

//teeb sellise asja valmis:

//    return query.Where(s => s.Name.Contains(SearchString)
//                          || s.Code.Contains(SearchString)
//                          || s.Id.Contains(SearchString)
//                          || s.Definition.Contains(SearchString)
//                          || s.ValidFrom.ToString().Contains(SearchString)          //et selle järgi saaks filtreerida peame  enne tegema stringiks ses tsee on tegelikult datetime tüüpi muutuja
//                          || s.ValidTo.ToString().Contains(SearchString));
//}