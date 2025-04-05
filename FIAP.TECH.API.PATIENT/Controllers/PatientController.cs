using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Patients;
using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.PATIENT.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IMapper mapper,
            IPatientService patientService)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientDto patientDTO)
        {
            try
            {
                var patient = _mapper.Map<Patient>(patientDTO);
                await _patientService.SendMessageAsync(patient);
                return Accepted();

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PatientUpdateDto patientDTO)
        {
            try
            {
                patientDTO.Id = id;
                var patient = _mapper.Map<Patient>(patientDTO);

                await _patientService.SendMessageAsync(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
