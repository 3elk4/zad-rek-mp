using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common;
using Microsoft.OpenApi.Extensions;
using System.Linq.Dynamic.Core;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.ListHandlers
{
    public class OrderedListHandler : AbstractHandler
    {
        private string sortParam { get; set; }
        private SortOrder sortOrder { get; set; }

        public OrderedListHandler(string sortParam, SortOrder sortOrder)
        {
            this.sortParam = sortParam;
            this.sortOrder = sortOrder;
        }

        public override object Handle(object request)
        {
            var items = ((List<Models.Tag>)request).AsQueryable();

            items = items.OrderBy($"{sortParam} {sortOrder.GetDisplayName()}");

            if (_nextHandler != null)
            {
                return _nextHandler.Handle(items.ToList());
            }
            else
            {
                return items.ToList();
            }
        }
    }
}
