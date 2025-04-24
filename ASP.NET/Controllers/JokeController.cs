using ASP.NET.Data;
using ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ASP.NET.Controllers;

public class JokeController : Controller
{
    private readonly ILogger<JokeController> _logger;
    private readonly ApplicationDbContext _context;
    
    public JokeController(ILogger<JokeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // GET
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Home()
    {
        var jokes = _context.jokes.ToList();
        List<Joke> jokeList = new List<Joke>();

        if (jokes != null)
        {
            foreach (var joke in jokes)
            {
                var jokeModel = new Joke()
                {
                    Id = joke.Id,
                    JokeQuestion = joke.JokeQuestion,
                    JokeAnswer = joke.JokeAnswer
                };
                jokeList.Add(jokeModel);
            }
        }
        
        return View(jokeList);
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

    public IActionResult DeleteJoke(int jokeId)
    {
        
        var jokeInDb = _context.jokes.SingleOrDefault(joke => joke.Id == jokeId + 1);
        if (jokeInDb != null)
        {
            _context.jokes.Remove(jokeInDb);
            _context.SaveChanges();
        }
        return RedirectToAction("Home");
    }

}