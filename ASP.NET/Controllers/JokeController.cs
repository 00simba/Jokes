using Microsoft.AspNetCore.Mvc;
using ASP.NET.Models;
using Npgsql;

namespace ASP.NET.Controllers;

public class JokeController : Controller
{
    private readonly ILogger<JokeController> _logger;

    public JokeController(ILogger<JokeController> logger)
    {
        _logger = logger;
    }
    

    // GET
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost("post-joke")]
    public string PostJoke()
    {
        return "posted joke";
    }
    
    [HttpGet("get-jokes")]
    public string GetJokes()
    {
        
        // connect to database 

        string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        using NpgsqlCommand cmd = new NpgsqlCommand("select * from jokes", connection);
        NpgsqlDataReader reader = cmd.ExecuteReader();
        
        
        
        return "HttpGet : get-jokes";
    }
    
}