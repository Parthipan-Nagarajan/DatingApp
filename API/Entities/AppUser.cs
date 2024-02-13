using System.ComponentModel.DataAnnotations;

namespace API;

public class AppUser
{
    public AppUser()
    {
    }

    [Key]
    public  int Id { get; set; }
    public required string UserName {get;set;}   

}
