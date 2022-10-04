using ApiGateway.Models;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillApi : ControllerBase
    {
        private readonly IProducerServices _producerServices;
        public BillApi(IProducerServices producerServices)
        {
            _producerServices = producerServices;

        }

        [HttpPost(Name = "ProducerBill")]
        public IActionResult Get(BillProducer bill)
        {
            _producerServices.SendOrderBill(bill.Topic, bill.Bill, bill.Id);
            return Ok(bill);
        }
    }
}