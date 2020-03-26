using Abc.Domain.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory //staatiliste klasside puhul ei ole klassi päritavad,
    {
        public static Measure Create(MeasureView v)
        {
            var o = new Measure();
            Copy.Members(v, o.Data); //võta v ja kopeeri see o.Datasse
           
            return o;
        }
        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView();
            Copy.Members(o.Data, v);
           
            return v;
        }
    }
}