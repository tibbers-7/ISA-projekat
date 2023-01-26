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
            storage = StoreLocation.Instance;
        }

        public async Task Consume(ConsumeContext<Location> context)
        {
             storage.isNew = true;
            if (storage.isNew) storage.Store(context.Message);
            await context.RespondAsync(context.Message);
            
            
        }
    }
}