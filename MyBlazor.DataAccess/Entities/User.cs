using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazor.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Fio { get; set; }
    public string Mail { get; set; }
    
    [NotMapped]
    public IEnumerable<Order> Orders { get; set; }
}