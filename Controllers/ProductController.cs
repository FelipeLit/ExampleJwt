using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

///[Authorize(Roles ="admin,user")]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ExampleContext _context;


    public ProductController(ExampleContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Product>>> GetProdutc()
    {
        var product =await _context.Product.ToListAsync();
        return Ok(product);
    }
}