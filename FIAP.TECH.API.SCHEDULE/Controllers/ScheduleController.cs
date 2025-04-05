using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.SCHEDULE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        public ScheduleController()
        {

        }














        //[HttpGet("get-by-region/{ddd}")]
        //public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
        //{
        //    try
        //    {
        //        var response = await _contactService.SendResponseMessageAsync(new ContactByDDD(ddd));
        //        if (!response.Success)
        //        {
        //            return BadRequest(response.Message);
        //        }

        //        return Ok(_mapper.Map<IEnumerable<ContactDto>>(response.Contacts));

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
    }
}
