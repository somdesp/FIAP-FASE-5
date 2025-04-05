using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateUpdatePatientConsumer : IConsumer<Patient>
    {
        private readonly IValidator<Patient> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueCreateUpdatePatientConsumer(
            IValidator<Patient> validator,
            AppDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _validator = validator;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Patient> context)
        {
            var patient = context.Message;
            Console.Clear();
            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    await _publishEndpoint.Publish(new ErrosProcess
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        Data = patient,
                        Success = false
                    });
                    return;
                }

                bool isUpdate = false;
                //Valida se e update ou create
                if (patient.Id > 0)
                {
                    var patientUpdate = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == patient.Id);
                    if (patientUpdate == null)
                    {
                        // Retornar erros de validação
                        await _publishEndpoint.Publish(new ErrosProcess
                        {
                            Errors = ["Paciente com ID informado não existe."],
                            Data = patient,
                            Success = false
                        });
                        return;
                    }

                    patientUpdate.Name = patient.Name;
                    patientUpdate.Password = patient.Password ?? patientUpdate.Password;

                    isUpdate = true;
                    _dbContext.Patients.Update(patientUpdate);
                }
                else
                    await _dbContext.Patients.AddAsync(patient);

                await _dbContext.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);
                Console.Clear();
                Console.WriteLine(!isUpdate ? "Paciente Inserido com Sucesso" : "Paciente Atualizado com Sucesso");
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ErrosProcess
                {
                    Errors = [ex.Message],
                    Data = patient,
                    Success = false
                });

                throw;
            }

        }
    }
}
