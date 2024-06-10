namespace MyBlazor.DTO;

public class OrderToShowDTO
{
    public DateTime CreatedAt { get; set; }
    public string GameTitle { get; set; }
    public int GameCost { get; set; }
    public string UserMail { get; set; }
}