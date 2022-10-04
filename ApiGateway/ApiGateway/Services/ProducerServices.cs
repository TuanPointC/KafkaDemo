using ApiGateway.Models;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Newtonsoft.Json;
using System.Net;

namespace ApiGateway.Services
{
    public class ProducerServices : IProducerServices
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly SchemaRegistryConfig _registry;

        public ProducerServices(SchemaRegistryConfig registry)
        {
            _registry = registry;
        }


        public async Task<bool> SendOrderBill(string topic, string message, string id)
        {
            ProducerConfig config = new()
            {
                BootstrapServers = _bootstrapServers,
                ClientId = Dns.GetHostName()
            };
            try
            {
                using var schema = new CachedSchemaRegistryClient(_registry);
                using var producer = new ProducerBuilder
                <Null,  string>(config)
                //.SetValueSerializer(new AvroSerializer<string>(schema))
                .Build();
                var result = await producer.ProduceAsync
                (topic, new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(new BillProducer
                    {
                        Id = id,
                        Bill = message,
                        Topic = topic
                    })
                });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return await Task.FromResult(false);
        }
    }
}
