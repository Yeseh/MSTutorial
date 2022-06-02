using System.ComponentModel.DataAnnotations;

namespace MSTutorial.CommandService.Models;

public class PlatformModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalId { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<CommandModel> Commands { get; set; } = new List<CommandModel>();
}
