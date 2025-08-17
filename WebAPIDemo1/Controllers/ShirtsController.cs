using Microsoft.AspNetCore.Mvc;
using WebAPIDemo1.Filters.ActionFilters;
using WebAPIDemo1.Models;
using WebAPIDemo1.Models.Repositories;


namespace WebAPIDemo1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {       
        
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }
        [HttpGet("{id}")]
        [Shirt_ValidationShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }
        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
             public IActionResult Createshirt([FromBody]Shirt shirt)
        {
           ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById),
                new { id = shirt.ShirtId },
                shirt);
                }
        [HttpPut("{id}")]
        [Shirt_ValidationShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        public IActionResult UpdateShirt(int id,Shirt shirt)
        {
            if (id != shirt.ShirtId) return BadRequest();

            try
            {
                ShirtRepository.UpdateShirt(shirt);
            }
            catch
            {
                if (!ShirtRepository.ShirtExists(id))
                    return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidationShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt =GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }

    }
}
