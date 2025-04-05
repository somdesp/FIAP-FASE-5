using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FluentValidation;
using MassTransit;

namespace FIAP.TECH.CORE.APPLICATION.Services.Contacts;

public class ScheduleService : IScheduleService
{
    private readonly IMapper _mapper;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;

    private readonly IBusControl _busControl;
    private readonly IRequestClient<SearchBySpecialty> _requestClient;

    public ScheduleService(IMapper mapper,
            IScheduleRepository scheduleRepository,
            IDoctorRepository doctorRepository,
            IBusControl busControl,
            IRequestClient<SearchBySpecialty> requestClient,
            IPatientRepository patientRepository
            )
    {
        _mapper = mapper;
        _scheduleRepository = scheduleRepository;
        _doctorRepository = doctorRepository;
        _busControl = busControl;
        _requestClient = requestClient;
        _patientRepository = patientRepository;
    }

    //Send message Generic
    public async Task SendMessageAsync(Schedule message)
    {
        try
        {
            await _busControl.Publish(message);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Update(ScheduleUpdateDto scheduleDTO)
    {
        var schedule = await _scheduleRepository.GetById(scheduleDTO.Id);
        if (schedule == null)
            throw new ValidationException("Agenda com ID informado não existe");

        schedule = _mapper.Map(scheduleDTO, schedule);

        //Valida se os dados estão corretos
        var resultValidation = new ScheduleInsertValidation();
        FluentValidation.Results.ValidationResult results = await resultValidation.ValidateAsync(schedule);

        if (results.Errors.Count != 0)
            throw new ValidationException(results.Errors);

        //valida o medico
        var doctor = await _doctorRepository.Search(x => x.Id == schedule.IdDoctor);
        if (doctor == null)
            throw new ValidationException("Medico inválido.");

        //valida o paciente
        if (schedule.IdPatient > 0)
        {
            var patient = await _patientRepository.Search(x => x.Id == schedule.IdPatient);
            if (patient == null)
                throw new ValidationException("Medico inválido.");
        }

        await _scheduleRepository.Update(schedule);
    }

    public async Task Delete(int id)
    {
        var contact = await _doctorRepository.GetById(id);

        if (contact is null)
            throw new InvalidOperationException("Contato com o ID informado não existe.");

        await _doctorRepository.Delete(contact);
    }

    public async Task<ScheduleResponse> SendResponseMessageAsync(SearchBySpecialty ddd)
    {
        var response = await _requestClient.GetResponse<ScheduleResponse>(ddd);
        return response.Message;
    }
}
