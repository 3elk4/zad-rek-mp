using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.ListHandlers
{
    public class PaginatedListHandler : AbstractHandler
    {
        private int pageSize { get; set; }
        private int currentPage { get; set; }

        public PaginatedListHandler(int pageSize, int currentPage)
        {
            this.pageSize = pageSize;
            this.currentPage = currentPage;
        }

        public override object Handle(object request)
        {
            var items = (List<Models.Tag>)request;
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
