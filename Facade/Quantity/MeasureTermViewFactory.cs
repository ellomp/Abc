using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureTermViewFactory
    {
        public static MeasureTerm Create(MeasureTermView v)
        {
            var d = new MeasureTermData();
            Copy.Members(v, d);

            return new MeasureTerm(d);
        }

        public static MeasureTermView Create(MeasureTerm o)
        {
            var v = new MeasureTermView();
            Copy.Members(o.Data, v);

            return v;
        }
    }
}
