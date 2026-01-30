using Application.Services.ItemsList;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsListController(IItemsListService itemsListService) : ControllerBase
    {
        [HttpGet("subskunames")]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await itemsListService.GetSubskuNames());
        }
    }
}
