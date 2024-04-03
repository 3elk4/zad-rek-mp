using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.DataPreprocessing.Common;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.CollectionHandlers
{
    public class PaginatedCollectionHandler : AbstractHandler
    {
        private int pageSize { get; set; }
        private int currentPage { get; set; }

        public PaginatedCollectionHandler(int pageSize, int currentPage)
        {
            this.pageSize = pageSize;
            this.currentPage = currentPage;
        }

        public override object Handle(object request)
        {
            var items = (List<Tag>)request;
            items = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            if (_nextHandler != null)
            {
                return _nextHandler.Handle(items.ToList());
            }
            else
            {
                return items;
            }
        }
    }
}
