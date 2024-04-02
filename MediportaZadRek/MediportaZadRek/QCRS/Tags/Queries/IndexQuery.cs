using MediatR;
using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.ListHandlers;
using MediportaZadRek.QCRS.Common.Interfaces;

namespace MediportaZadRek.QCRS.Tags.Queries
{
    public record IndexQuery : IRequest<TagsWithPaginationDetails>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public SortOrder SortOrder { get; set; }
    }

    public class IndexQueryHandler : IRequestHandler<IndexQuery, TagsWithPaginationDetails>
    {
        private readonly IDbContext _dbContext;

        public IndexQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TagsWithPaginationDetails> Handle(IndexQuery request, CancellationToken cancellationToken)
        {
            var tags = _dbContext.Tags.ToList();
            var total = tags.Count();

            tags = (List<Tag>)new CollectionPreprocessor()
                       .AddHandler(new OrderedListHandler(request.SortParam, request.SortOrder))
                       .AddHandler(new PaginatedListHandler(request.PageSize, request.CurrentPage))
                       .Process(tags);

            return Task.FromResult(new TagsWithPaginationDetails()
            {
                Items = tags,
                Total = total,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            });
        }
    }
}
