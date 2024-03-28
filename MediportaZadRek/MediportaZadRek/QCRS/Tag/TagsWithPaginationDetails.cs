namespace MediportaZadRek.QCRS.Tag
{
    public class TagsWithPaginationDetails
    {
        public List<Models.Tag> Items { get; set; } = new List<Models.Tag>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
