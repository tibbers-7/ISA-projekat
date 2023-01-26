namespace BloodBankAPI
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Model;

    public class LocationConsumer :IConsumer<Location>{

        private StoreLocation storage;
        public LocationConsumer()
        {
            storage = new StoreLocation();
        }

        public async Task Consume(ConsumeContext<Location> context)
        {
            await context.RespondAsync(context.Message);
            storage.Store(context.Message);
            
        }
    }
}