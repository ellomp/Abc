using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Infra.Quantity {

    public sealed class SystemOfUnitsRepository : UniqueEntityRepository<SystemOfUnits, SystemOfUnitsData>,
        ISystemOfUnitsRepository {

        public SystemOfUnitsRepository() : this(null) { }
        public SystemOfUnitsRepository(QuantityDbContext c) : base(c, c?.SystemsOfUnits) { }

        protected internal override SystemOfUnits toDomainObject(SystemOfUnitsData d) => new SystemOfUnits(d);


    }

}

