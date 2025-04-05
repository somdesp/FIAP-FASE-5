using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateUpdateScheduleConsumer : IConsumer<Schedule>
    {
        private readonly IValidator<Schedule> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueCreateUpdateScheduleConsumer(
            IValidator<Schedule> validator,
            AppDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _validator = validator;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Schedule> context)
        {
            var schedule = context.Message;
            Console.Clear();
            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    await _publishEndpoint.Publish(new ErrosProcess
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        Data = schedule,
                        Success = false
                    });
                    return;
                }

                //valida doctor
                var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == schedule.IdDoctor);
                if (doctor == null)
                {
                    // Retornar erros de validação
                    await _publishEndpoint.Publish(new ErrosProcess
                    {
                        Errors = ["Medico inexistente na base de dados."],
                        Data = schedule,
                        Success = false
                    });
                    return;
                }

                schedule.IdDoctor = doctor.Id;
                bool isUpdate = false;
                //Valida se e uma nova agenda
                if (schedule.Id > 0)
                {
                    var scheduleUpdate = await _dbContext.Schedule.FirstOrDefaultAsync(x => x.Id == schedule.Id);
                    if (scheduleUpdate == null)
                    {
                        // Retornar erros de validação
                        await _publishEndpoint.Publish(new ErrosProcess
                        {
                            Errors = ["Agenda com ID informado não existe."],
                            Data = schedule,
                            Success = false
                        });
                        return;
                    }

                    //valida se possui paciente
                    if (schedule.IdPatient.HasValue)
                    {
                        var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == schedule.IdPatient);
                        if (patient == null)
                        {
                            // Retornar erros de validação
                            await _publishEndpoint.Publish(new ErrosProcess
                            {
                                Errors = ["Paciente com ID informado não existe."],
                                Data = schedule,
                                Success = false
                            });
                            return;
                        }

                        scheduleUpdate.IdPatient = patient.Id;

                    }


                    isUpdate = true;
                    _dbContext.Schedule.Update(scheduleUpdate);
                }
                else
                    await _dbContext.Schedule.AddAsync(schedule);

                await _dbContext.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);
                Console.Clear();
                Console.WriteLine(!isUpdate ? "Agenda Inserida com Sucesso" : "Agenda Atualizada com Sucesso");
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ErrosProcess
                {
                    Errors = [ex.Message],
                    Data = schedule,
                    Success = false
                });
                //Console.WriteLine(ex, "Error processing Schedule updated event for Schedule {ScheduleId}", message.Id);
                throw;
            }

        }
    }
}
