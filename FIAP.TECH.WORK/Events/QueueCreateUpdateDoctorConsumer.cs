using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateUpdateDoctorConsumer : IConsumer<Doctor>
    {
        private readonly IValidator<Doctor> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueCreateUpdateDoctorConsumer(
            IValidator<Doctor> validator,
            AppDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _validator = validator;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Doctor> context)
        {
            var doctor = context.Message;
            Console.Clear();
            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    await _publishEndpoint.Publish(new ErrosProcess
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        Data = doctor,
                        Success = false
                    });
                    return;
                }

                //valida a especialidade
                var specialty = await _dbContext.Specialty.FirstOrDefaultAsync(x => x.Id == doctor.IdSpecialty);
                if (specialty == null)
                {
                    // Retornar erros de validação
                    await _publishEndpoint.Publish(new ErrosProcess
                    {
                        Errors = ["Especialidade inexistente na base de dados."],
                        Data = doctor,
                        Success = false
                    });
                    return;
                }

                doctor.IdSpecialty = specialty.Id;
                bool isUpdate = false;
                //Valida se e update ou create
                if (doctor.Id > 0)
                {
                    var doctorUpdate = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == doctor.Id);
                    if (doctorUpdate == null)
                    {
                        // Retornar erros de validação
                        await _publishEndpoint.Publish(new ErrosProcess
                        {
                            Errors = ["Medico com ID informado não existe."],
                            Data = doctor,
                            Success = false
                        });
                        return;
                    }

                    doctorUpdate.Name = doctor.Name;
                    doctorUpdate.IdSpecialty = doctor.IdSpecialty;
                    doctorUpdate.Password = doctor.Password ?? doctorUpdate.Password;

                    isUpdate = true;
                    _dbContext.Doctors.Update(doctorUpdate);
                }
                else
                    await _dbContext.Doctors.AddAsync(doctor);

                await _dbContext.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);
                Console.Clear();
                Console.WriteLine(!isUpdate ? "Medico Inserido com Sucesso" : "Medico Atualizado com Sucesso");
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ErrosProcess
                {
                    Errors = [ex.Message],
                    Data = doctor,
                    Success = false
                });
                //Console.WriteLine(ex, "Error processing Doctor updated event for Doctor {DoctorId}", message.Id);
                throw;
            }

        }
    }
}
