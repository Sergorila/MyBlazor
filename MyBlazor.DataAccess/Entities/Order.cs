namespace MyBlazor.DataAccess.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
}