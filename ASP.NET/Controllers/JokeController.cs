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
    public string PostJoke(string jokeQuestion, string jokeAnswer)
    {
        // connect to database 

        string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        using NpgsqlCommand cmd = new NpgsqlCommand("insert into jokes (joke_question, joke_answer) values (@joke_question, @joke_answer)", connection);

        cmd.Parameters.AddWithValue("@joke_question", jokeQuestion);
        cmd.Parameters.AddWithValue("@joke_answer", jokeAnswer);
        
        cmd.ExecuteNonQuery();
        
        return "HttpPost : joke posted";
    }
    
    [HttpGet("get-jokes")]
    public string GetJokes()
    {
        
        string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        using NpgsqlCommand cmd = new NpgsqlCommand("select * from jokes", connection);
        NpgsqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader["joke_question"]);
            Console.WriteLine(reader["joke_answer"]);
        }

        return "HttpGet : get-jokes";
    }
    
}