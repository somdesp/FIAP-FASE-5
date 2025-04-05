using FIAP.TECH.CORE.DOMAIN.Models;
using MassTransit;

namespace FIAP.TECH.WORK.Events
{
    public class QueueErrosConsumer : IConsumer<ErrosProcess>
    {
        public async Task Consume(ConsumeContext<ErrosProcess> context)
        {
            var message = context.Message;

            try
            {
                await Task.Delay(1000);
                Console.WriteLine(message.Data != null ? message.Data.ToString() : "");
                Console.WriteLine(message.Data != null ? $"Error for model {message.Data}: {string.Join(", ", message.Errors)}" : "");

                await Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
