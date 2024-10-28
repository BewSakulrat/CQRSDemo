using Application.Commands.Customers.CreateCustomer;
using Application.Commands.Customers.UpdateCustomer;
using Application.Common.Responses;
using Application.DTOs.Customer;
using Application.Queries.Customers.GetAllCustomers;
using Application.Queries.Customers.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var customers = await _mediator.Send(query);
            var response = new ApiResponse<IEnumerable<CustomerDto>>(customers);
            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(customerId));

            if (customer == null)
            {
                return NotFound();
            }
            
            var response = new ApiResponse<CustomerDto>(customer);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, null);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = customerId;
            var updatedCustomer = await _mediator.Send(command);
            if (updatedCustomer == null)
            {
                return NotFound(new { message = $"Customer with ID {customerId} not found." });
            }
            return Ok(new ApiResponse<CustomerDto>(updatedCustomer));
        }
    }
}
