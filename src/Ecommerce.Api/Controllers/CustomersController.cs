﻿using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Core.Exceptions;

namespace Ecommerce.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerApplicationService _customerApplicationService;

    public CustomersController(ICustomerApplicationService customerApplicationService)
    {
        _customerApplicationService = customerApplicationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreatedCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            _customerApplicationService.SaveCustomer(customerDto);
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(ex.Message);
        }


        return Created("", customerDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCustomer()
    {
        var customers = _customerApplicationService.GetCustomers();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCustomer([FromRoute] string id)
    {
        var formattedId = Uri.UnescapeDataString(id);
        var customers = _customerApplicationService.GetCustomerById(formattedId);

        if(customers == null)
            return NotFound();

        return Ok(customers);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteCustomer([FromRoute] string id)
    {
        var formattedId = Uri.UnescapeDataString(id);
        _customerApplicationService.DeleteCustomer(formattedId);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            _customerApplicationService.UpdateCustomer(customerDto);
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(ex.Message);
        }

        return NoContent();
    }

}
