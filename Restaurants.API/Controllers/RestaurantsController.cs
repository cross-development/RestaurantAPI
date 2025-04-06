using Microsoft.AspNetCore.Mvc;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());

        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery { Id = id });

        return restaurant is null ? NotFound() : Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<RestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantCommand command)
    {
        command.Id = id;

        var isUpdated = await mediator.Send(command);

        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<RestaurantDto>> DeleteRestaurant(int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand { Id = id });

        return isDeleted ? NoContent() : NotFound();
    }
}
