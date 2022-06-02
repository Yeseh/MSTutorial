﻿using System.ComponentModel.DataAnnotations;

namespace MSTutorial.CommandService.Models;

public class CommandModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }

    [Required]
    public int PlatformId { get; set; }

    public PlatformModel Platform { get; set;  }
}
