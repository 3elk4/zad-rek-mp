using MediportaZadRek.Data;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common;

namespace MediportaZadRek.QCRS.Tag.Queries
{
    public record IndexQuery
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public string SortOrder { get; set; }
    }

    public class IndexQueryPreprocessor
    {
        private List<IHandler> handlers;
        public IndexQueryPreprocessor()
        {
            handlers = new List<IHandler>();
        }

        public IndexQueryPreprocessor AddHandler(IHandler handler)
        {
            handlers.Add(handler);
            return this;
        }

        public object Process(object obj)
        {
            SetChainOfHandlers();
            return Handle(obj);
        }

        private void SetChainOfHandlers()
        {
            for (int i = 0; i < handlers.Count() - 1; ++i)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }
        }

        private object Handle(object obj)
        {
            return handlers.First().Handle(obj);
        }
    }

    public class IndexQueryHandler
    {
        private readonly AppDbContext _dbContext;

        public IndexQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TagsWithPaginationDetails Handle(IndexQuery request)
        {
            var tags = _dbContext.Tags.ToList();
            var total = tags.Count();

            tags = (List<Models.Tag>)new IndexQueryPreprocessor()
                       .AddHandler(new OrderedListHandler(request.SortParam, request.SortOrder))
                       .AddHandler(new PaginatedListHandler(request.PageSize, request.CurrentPage))
                       .Process(tags);

            return new TagsWithPaginationDetails()
            {
                Items = tags,
                Total = total,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            };

        }
    }
}
