using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RetoStore.Persistence;

public class RetoStoreUserIdentity : IdentityUser
{
    [StringLength(100)]
    public string FirstName { get; set; } = default!;
    [StringLength(100)]
    public string LastName { get; set; } = default!;
    public int Age { get; set; }

    public DocumentTypeEnum DocumentType { get; set; }
    [StringLength(20)]
    public string DocumentNumber { get; set; } = default!;
}

public enum DocumentTypeEnum : short
{
    Dni,
    Passport
}
