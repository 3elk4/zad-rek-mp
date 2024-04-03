using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.DataPreprocessing.Common;
using Microsoft.OpenApi.Extensions;
using System.Linq.Dynamic.Core;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.CollectionHandlers
{
    public class OrderedCollectionHandler : AbstractHandler
    {
        private string sortParam { get; set; }
        private SortOrder sortOrder { get; set; }

        public OrderedCollectionHandler(string sortParam, SortOrder sortOrder)
        {
            this.sortParam = sortParam;
            this.sortOrder = sortOrder;
        }

        public override object Handle(object request)
        {
            var items = ((List<Tag>)request).AsQueryable();

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
