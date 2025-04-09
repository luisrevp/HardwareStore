using Microsoft.AspNetCore.Mvc;
using HardwareStore.BE.Services;
using HardwareStore.BE.Entities;

namespace HardwareStore.BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IUserCartService _service;
        public CartController(IUserCartService service) 
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("getCartArticles/{id}")]
        public async Task<ActionResult> GetCart(string id)
        {
            Cart cart = await _service.GetCartByUser(id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
