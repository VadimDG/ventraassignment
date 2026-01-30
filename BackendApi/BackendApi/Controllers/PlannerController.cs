using Application.Dtos;
using Application.Services.Calculate;
using Application.Services.Items;
using Application.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlannerController(ICalculateService calculateService, IItemsService itemsService, ILogger<PlannerController> logger) : ControllerBase
    {
        const string GENERIC_PLANNER_CALCULATION_ERROR_MESSAGE = "Planner calculation error";
        const string GENERIC_PLANNER_UPDATE_ERROR_MESSAGE = "Planner update error";

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int[] selectedSubSkus)
        {
            try
            {
                var data = await calculateService.Calculate(selectedSubSkus);

                return Ok(new
                {
                    Data = data,
                    Metadata = MetadataService.Metadata<SkuDto>()
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GENERIC_PLANNER_CALCULATION_ERROR_MESSAGE);
                return BadRequest(GENERIC_PLANNER_CALCULATION_ERROR_MESSAGE);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] SubskuUpdateDto[] subSkus)
        {
            try
            {
                await itemsService.UpdateSubskuAsync(subSkus);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GENERIC_PLANNER_UPDATE_ERROR_MESSAGE);
                return BadRequest(GENERIC_PLANNER_UPDATE_ERROR_MESSAGE);
            }
        }
    }
}
