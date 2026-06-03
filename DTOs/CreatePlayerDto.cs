using System.ComponentModel.DataAnnotations;

namespace AtelierTest.DTOs;

public class CreatePlayerDto
{
    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    public string Shortname { get; set; } = string.Empty;

    [Required]
    public string Sex { get; set; } = string.Empty;

    [Required]
    public string CountryCode { get; set; } = string.Empty;

    public string CountryPicture { get; set; } = string.Empty;

    public string Picture { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Rank { get; set; }

    [Range(0, int.MaxValue)]
    public int Points { get; set; }

    [Range(1, 500000)]
    public int Weight { get; set; }

    [Range(50, 250)]
    public int Height { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
}