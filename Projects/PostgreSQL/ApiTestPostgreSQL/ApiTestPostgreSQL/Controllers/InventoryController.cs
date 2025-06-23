using ApiTestPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTestPostgreSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly InventoryRepository _inventoryRepository;

        public InventoryController(ILogger<InventoryController> logger, InventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        [HttpPost("initialize")]
        public IActionResult Initialize()
        {
            _inventoryRepository.Initialize();
            return Ok();
        }

        [HttpGet]
        public IEnumerable<InventoryItem> Get()
        {
            return _inventoryRepository.List();
        }
    }
}
