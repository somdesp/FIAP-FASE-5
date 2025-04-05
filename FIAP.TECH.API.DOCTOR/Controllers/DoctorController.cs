using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Doctors;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.DOCTOR.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IMapper mapper,
            IDoctorService doctorService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorDto doctorDTO)
        {
            try
            {
                var doctor = _mapper.Map<Doctor>(doctorDTO);
                await _doctorService.SendMessageAsync(doctor);
                return Accepted();

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DoctorUpdateDto doctorDTO)
        {
            try
            {
                doctorDTO.Id = id;
                var doctor = _mapper.Map<Doctor>(doctorDTO);

                await _doctorService.SendMessageAsync(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("SearchBySpecialty")]
        public async Task<IActionResult> SearchBySpecialty([FromBody] string specialty)
        {
            try
            {
                var response = await _doctorService.SendResponseMessageAsync(new SearchBySpecialty(specialty));
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return Ok(_mapper.Map<IEnumerable<Doctor>>(response.Doctors));

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
