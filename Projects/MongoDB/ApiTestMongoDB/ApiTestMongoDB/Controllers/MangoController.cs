using ApiTestMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ApiTestMongoDB.Controllers;

[ApiController]
[Route("[controller]")]
public class MangoController : ControllerBase
{
    private readonly MongoClient _mongoClient;

    public MangoController(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    [HttpGet]
    public async Task<IActionResult> ListMangos()
    {
        var db = _mongoClient.GetDatabase("my_mangolicious_grocery_store");
        var collection = db.GetCollection<Mango>("mangos");

        var mangos = await collection.AsQueryable().ToListAsync();
        return Ok(mangos);
    }

    [HttpPost]
    public async Task<IActionResult> AddMango(Mango mango)
    {
        mango.Id = Guid.NewGuid().ToString();

        var db = _mongoClient.GetDatabase("my_mangolicious_grocery_store");
        var collection = db.GetCollection<Mango>("mangos");

        await collection.InsertOneAsync(mango);
        return Ok();
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> Initialize()
    {
        var db = _mongoClient.GetDatabase("my_mangolicious_grocery_store");
        await db.CreateCollectionAsync("mangos");

        return Ok();
    }
}
