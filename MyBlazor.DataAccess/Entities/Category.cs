using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazor.DataAccess.Entities;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public IEnumerable<Game> Games { get; set; }
}