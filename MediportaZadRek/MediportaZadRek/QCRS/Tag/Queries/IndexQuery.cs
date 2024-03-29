using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.ListHandlers;
using MediportaZadRek.QCRS.Common.Interfaces;

namespace MediportaZadRek.QCRS.Tag.Queries
{
    public record IndexQuery
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public SortOrder SortOrder { get; set; }
    }

    public class IndexQueryHandler
    {
        private readonly IDbContext _dbContext;

        public IndexQueryHandler(IDbContext dbContext)
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
