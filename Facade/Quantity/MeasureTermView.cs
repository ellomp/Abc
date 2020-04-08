using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Quantity
{
    public sealed class MeasureTermView : CommonTermView
    {
        [Required] //siia lisaisin viimasena piho järgi
        [DisplayName("Measure")]
        public string MasterId { get; set; }

        public string GetId()
        {
            return $"{MasterId}.{TermId}";
        }
    }
}