using Abc.Data.Common;

namespace Abc.Data.Quantity
{
    public abstract class CommonTermData : PeriodData
    {
        public string MasterId { get; set; }          //ükskõik kas hoiad Id nimele v millelegi muule
        public string TermId { get; set; }
        public int Power { get; set; }

    }
}
