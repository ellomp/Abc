using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Facade.Quantity
{
    public static class UnitTermViewFactory
    {
        public static UnitTerm Create(UnitTermView v)
        {
            var d = new UnitTermData();
            Copy.Members(v, d); 

            return new UnitTerm(d);

        }

        public static UnitTermView Create(UnitTerm o)
        {
            var v = new UnitTermView();
            Copy.Members(o.Data, v);

            return v;

        }
    }
}
