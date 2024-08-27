using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseMaterialController : ControllerBase
    {
        private readonly IPurchaseMaterialService _purchaseMaterialService;

        public PurchaseMaterialController(IPurchaseMaterialService purchaseMaterialService)
        {
            _purchaseMaterialService = purchaseMaterialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _purchaseMaterialService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _purchaseMaterialService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchaseMaterialService.DeleteAsync(id);
            return Ok("Purchase material delete success");
        }
    }
}
