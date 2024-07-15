using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;
using Para.Data.UnitOfWork;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<List<Customer>> Get()
        {
            var entityList = await unitOfWork.CustomerRepository.GetAll();
            return entityList;
        }

        [HttpGet("{customerId}")]
        public async Task<Customer> Get(long customerId)
        {
            var entity = await unitOfWork.CustomerRepository.GetById(customerId);
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Customer value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await unitOfWork.CustomerRepository.Insert(value);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { customerId = value.Id }, value);
        }

        [HttpPut]
        public async Task<ActionResult> Put(long customerId, [FromBody] Customer value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await unitOfWork.CustomerRepository.Update(value);
            await unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task Delete(long customerId)
        {
            await unitOfWork.CustomerRepository.Delete(customerId);
            await unitOfWork.Complete();
        }

        [HttpGet("GetByName/{name}")]
        public async Task<List<Customer>> GetByName(string name)
        {
            var customers = await unitOfWork.CustomerRepository.Where(x => x.FirstName == name);
            return customers;
        }


        [HttpGet("GetWithDetails")]
        public async Task<List<Customer>> GetWithDetails()
        {
            var customers = await unitOfWork.CustomerRepository.Include(x => x.CustomerDetail, x => x.CustomerAddresses, x => x.CustomerPhones);
            return customers;
        }

        [HttpGet("GetByNameWithDetails/{name}")]
        public async Task<List<Customer>> GetByNameWithDetails(string name)
        {
            var customers = await unitOfWork.CustomerRepository.WhereWithIncludes(x => x.FirstName == name, x => x.CustomerDetail, x => x.CustomerAddresses, x => x.CustomerPhones);
            return customers;
        }
    }
}