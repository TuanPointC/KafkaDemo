using Bill;
using Confluent.Kafka;
using Newtonsoft.Json;

var topic = "bill";
var groupId = "test_group";
var bootstrapServers = "localhost:9092";

var config = new ConsumerConfig
{
    GroupId = groupId,
    BootstrapServers = bootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableSslCertificateVerification = false
};
var client = new HttpClient();


try
{
    using (var consumerBuilder = new ConsumerBuilder
    <Ignore, string>(config).Build())
    {
        consumerBuilder.Subscribe(topic);
        var cancelToken = new CancellationTokenSource();

        try
        {
            while (true)
            {
                var consumer = consumerBuilder.Consume(cancelToken.Token);
                var bill = JsonConvert.DeserializeObject<BillModel>(consumer.Message.Value);
                var userName = await client.GetStringAsync($"https://localhost:7075/Auth?id={bill?.Id}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{userName} with bill {bill?.Bill}$");
                Console.ResetColor();
            }
        }
        catch (OperationCanceledException)
        {
            consumerBuilder.Close();
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}