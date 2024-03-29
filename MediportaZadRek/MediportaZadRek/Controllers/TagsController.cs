using MediportaZadRek.Data;
using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Tag;
using MediportaZadRek.QCRS.Tag.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediportaZadRek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ILogger<TagsController> _logger;
        private readonly AppDbContext _context;
        private readonly TagsContextInitializer _initializer;

        public TagsController(ILogger<TagsController> logger, AppDbContext context, TagsContextInitializer initializer)
        {
            _logger = logger;
            _context = context;
            _initializer = initializer;
        }

        /// <summary>
        /// Gets paginated collection of sorted tags.
        /// </summary>
        /// <param name="currentPage"> Current page. </param>
        /// <param name="pageSize"> Tags per page. </param>
        /// <param name="sortParam"> Tag parameter to sort by. </param>
        /// <param name="sortOrder"> Collection order. </param>
        /// <response code="200"> Returns collection of tags with pagination details </response>
        /// <response code="400"> If sortParam or sortOrder is wrong. </response>
        [HttpGet(Name = "Get Tags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TagsWithPaginationDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 10, string sortParam = "Name", SortOrder sortOrder = SortOrder.asc)
        {
            try
            {
                var request = new IndexQuery() { CurrentPage = currentPage, PageSize = pageSize, SortParam = sortParam, SortOrder = sortOrder };
                var handler = new IndexQueryHandler(_context); //todo: zastanowić się czy tak do tego podejść? czy jednak jakiś mediator?
                var result = handler.Handle(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting tags.");
                return BadRequest();
            }
        }

        /// <summary>
        /// Refreshes tags in database.
        /// </summary>
        /// <response code="200"> Tags in database are cleared and reseed </response>
        /// <response code="500"> If any server problem with refreshing occurs </response>
        [HttpPut(Name = "Refresh Tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                await _initializer.RefreshAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing tags.");
                return StatusCode(500);
            }
        }
    }
}
