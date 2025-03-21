﻿using System.ComponentModel.DataAnnotations;

namespace RetoStore.Dto.Request;

public class RegisterRequestDto
{
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; } = default!;

    [Required]
    [StringLength(200)]
    public string LastName { get; set; } = default!;

    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(200)]
    public string DocumnetNumber { get; set; } = default!;

    public int DocumentType { get; set; }
    public int Age { get; set; }

    [Required]
    public string Password { get; set; } = default!;

    [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir.")]
    public string ConfirmPassword { get; set; } = default!;
}
