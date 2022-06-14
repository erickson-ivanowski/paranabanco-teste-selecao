using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Query;
using ParanaBanco.Service.Customers.Application.RequestModels;
using ParanaBanco.Service.Customers.Application.ViewModels;

namespace ParanaBanco.Service.Customers.Api.Controllers
{
    [ApiController()]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Get all customers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAll()
        {
            var result = await _mediator.Send(new GetCustomersQuery());

            return Ok(result);
        }

        /// <summary>
        /// Get a customer by email
        /// </summary>
        [HttpGet("{email}",Name = "Get Customer by email")]
        public async Task<ActionResult<CustomerViewModel>> Get(string email)
        {
            var query = new GetCustomerByEmailQuery
            {
                Email = email
            };

            if (await _mediator.Send(query) is var result && result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Create a customer
        /// </summary>
        [HttpPost(Name = "Create new customer")]
        public async Task<ActionResult> Create(CreateCustomerCommand command)
        {
            await _mediator.Send(command);

            return Created("customers/{email}", command.Email);
        }

        /// <summary>
        /// Update a customer
        /// </summary>
        [HttpPut("{email}",Name = "Update customer")]
        public async Task<ActionResult> Update(string email, [FromBody] UpdateCustomerRequest request)
        {
            var command = new UpdateCustomerCommand
            {
                Email = email,
                NewEmail = request.NewEmail,
                NewFullName = request.NewFullName
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        [HttpDelete("{email}",Name = "Delete customer")]
        public async Task<ActionResult> Delete(string email)
        {
            var command = new DeleteCustomerCommand
            {
                Email = email
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
