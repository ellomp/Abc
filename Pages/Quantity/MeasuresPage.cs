using System.Collections.Generic;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity
{

    public abstract class MeasuresPage : CommonPage<IMeasuresRepository, Measure, MeasureView, MeasureData>
    {

        protected internal readonly IMeasureTermsRepository terms;
        protected internal MeasuresPage(IMeasuresRepository r, IMeasureTermsRepository t) : base(r)
        {
            PageTitle = "Measures";
            Terms = new List<MeasureTermView>();
            terms = t;
        }
        public IList<MeasureTermView> Terms { get; }


        public override string ItemId => Item?.Id ?? string.Empty;

        protected internal override string getPageUrl() => "/Quantity/Measures";

        protected internal override Measure ToObject(MeasureView view) => MeasureViewFactory.Create(view);

        protected internal override MeasureView ToView(Measure obj) => MeasureViewFactory.Create(obj);


        public void LoadDetails(MeasureView item)
        {
            Terms.Clear();

            if (item is null) return;
            //loe ainult olemasoleva MasterIdga asjad sisse
            terms.FixedFilter = GetMember.Name<MeasureTermData>(x => x.MasterId);
            terms.FixedValue = item.Id;
            var list = terms.Get().GetAwaiter().GetResult();

            foreach (var e in list)
            {
                Terms.Add(MeasureTermViewFactory.Create(e));
            }
        }

    }

}