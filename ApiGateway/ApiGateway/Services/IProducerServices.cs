namespace ApiGateway.Services
{
    public interface IProducerServices
    {
        public Task<bool> SendOrderBill(string topic, string message,string id);
    }
}
