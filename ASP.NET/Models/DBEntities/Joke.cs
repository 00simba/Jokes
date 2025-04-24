using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASP.NET.Models;


[Table("jokes")]
public class Joke
{
    [Column("joke_id")]
    public int Id { get; set; }
    [Column("joke_question")]
    [MaxLength(1000)]
    public string JokeQuestion { get; set; }
    [Column("joke_answer")]
    [MaxLength(1000)]
    public string JokeAnswer { get; set; }

    public Joke()
    {
        
    }
    
}