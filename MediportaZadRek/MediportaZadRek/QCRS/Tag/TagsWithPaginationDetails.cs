using System.ComponentModel.DataAnnotations;

namespace MediportaZadRek.QCRS.Tag
{
    public class TagsWithPaginationDetails
    {
        public List<Models.Tag> Items { get; set; } = new List<Models.Tag>();

        [Range(0, 2147483647)]
        public int CurrentPage { get; set; }

        [Range(0, 2147483647)]
        public int PageSize { get; set; }

        [Range(0, 2147483647)]
        public int Total { get; set; }
    }
}
