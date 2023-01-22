namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;


    public class CheckLocationConsumer :
        IConsumer<CheckLocation>
    { 

        public CheckLocationConsumer()
        {
        }

        public Task Consume(ConsumeContext<CheckLocation> context)
        {
            LocationRepo repo = new LocationRepo();
            repo.Init();
            
            return context.RespondAsync(repo.GetLocation(context.Message.Id));
        }
    }
}