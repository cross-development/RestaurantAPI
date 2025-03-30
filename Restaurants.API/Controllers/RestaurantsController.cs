using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Restaurant>> GetById(int id)
    {
        var restaurant = await restaurantsService.GetRestaurantById(id);

        if (restaurant is null) return NotFound();

        return Ok(restaurant);
    }
}
