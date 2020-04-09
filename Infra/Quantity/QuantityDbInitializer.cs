using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Core.Units;
using Abc.Data.Common;
using Abc.Data.Quantity;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantity
{

    public static class QuantityDbInitializer
    {

        public static void Initialize(QuantityDbContext db)
        {
            Initialize(SystemOfUnits.Units, db);
            Initialize(Area.Measure, Area.Units, db);
            Initialize(Counter.Measure, Counter.Units, db);
            Initialize(Current.Measure, Current.Units, db);
            Initialize(Distance.Measure, Distance.Units, db);
            Initialize(Luminos.Measure, Luminos.Units, db);
            Initialize(ManHour.Measure, ManHour.Units, db);
            Initialize(Mass.Measure, Mass.Units, db);
            Initialize(Persentage.Measure, Persentage.Units, db);
            Initialize(Substance.Measure, Substance.Units, db);
            Initialize(Temperature.Measure, Temperature.Units, db);
            Initialize(Time.Measure, Time.Units, db);
            Initialize(Volume.Measure, Volume.Units, db);

        }

        private static void Initialize(IEnumerable<Core.Units.Data> data, QuantityDbContext db)
        {
            foreach (var d in from d in data
                              let o = db.SystemsOfUnits.FirstOrDefaultAsync(m => m.Id == d.Id).GetAwaiter().GetResult()
                              where o is null
                              select d)
            {
                db.SystemsOfUnits.Add(
                    new SystemOfUnitsData
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Name = d.Name,
                        Definition = d.Definition
                    });
            }
        }

        private static void Initialize(Core.Units.Data measure, List<Core.Units.Data> units, QuantityDbContext db)
        {
            AddMeasure(measure, db);
            AddTerms(measure, db.MeasureTerms);
            AddUnits(units, measure.Id, db);
            AddTerms(units, db);
            AddUnitFactors(units, SystemOfUnits.SiSystemId, db);
            db.SaveChanges();

        }

        private static void AddUnitFactors(List<Core.Units.Data> units, string siSystemId, QuantityDbContext db)
        {
            foreach (var d in from d in units where !(Math.Abs(d.Factor) < double.Epsilon) let o = db.UnitFactors.FirstOrDefaultAsync(
                m => m.SystemOfUnitsId == siSystemId && m.UnitId == d.Id).GetAwaiter().GetResult() where o is null select d)
            {
                db.UnitFactors.Add(
                    new UnitFactorData()
                    {
                        SystemOfUnitsId = siSystemId,
                        UnitId = d.Id,
                        Factor = d.Factor
                    });

            }
        }

        private static void AddTerms(List<Core.Units.Data> units, QuantityDbContext db)
        {
            foreach (var d in units)
                AddTerms(d, db.UnitTerms);

        }

        private static void AddTerms<T>(Core.Units.Data measure, DbSet<T> db) where T : CommonTermData, new()
        {
            foreach (var d in from d in measure.Terms let o = db.FirstOrDefaultAsync(
                m => m.MasterId == measure.Id && m.TermId == d.TermId).GetAwaiter().GetResult() where o is null select d)
            {
                db.Add(
                    new T
                    {
                        MasterId = measure.Id,
                        TermId = d.TermId,
                        Power = d.Power
                    });
            }
        }

        private static void AddMeasure(Core.Units.Data measure, QuantityDbContext db)
        {
            var o = GetItem(db.Measures, measure.Id);

            if (!(o is null)) return;
            db.Measures.Add(
                new MeasureData
                {
                    Id = measure.Id,
                    Code = measure.Code,
                    Name = measure.Name,
                    Definition = measure.Definition
                });

        }

        private static T GetItem<T>(IQueryable<T> set, string id) where T : UniqueEntityData
            => set.FirstOrDefaultAsync(m => m.Id == id).GetAwaiter().GetResult();

        private static void AddUnits(IEnumerable<Core.Units.Data> units, string measureId, QuantityDbContext db)
        {
            foreach (var d in from d in units
                              let o = GetItem(db.Units, d.Id)
                              where o is null
                              select d)
            {
                db.Units.Add(
                    new UnitData
                    {
                        MeasureId = measureId,
                        Id = d.Id,
                        Code = d.Code,
                        Name = d.Name,
                        Definition = d.Definition
                    });

            }
        }

    }

}
