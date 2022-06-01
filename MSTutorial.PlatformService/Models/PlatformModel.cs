using System.ComponentModel.DataAnnotations;

namespace MSTutorial.PlatformService.Models;

public class PlatformModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Publisher { get; set; }

    [Required]
    public string Cost { get; set; }
}
