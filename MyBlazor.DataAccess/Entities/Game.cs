using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazor.DataAccess.Entities;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Cost { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}