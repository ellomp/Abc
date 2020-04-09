using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory //staatiliste klasside puhul ei ole klassi päritavad
    {
        public static Measure Create(MeasureView v)
        {
            var d = new MeasureData();
            Copy.Members(v, d); //võta v ja kopeeri see dsse

            return new Measure(d);

        }

        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView();
            if (!(o?.Data is null))
                Copy.Members(o.Data, v);

            return v;

        }
    }
}