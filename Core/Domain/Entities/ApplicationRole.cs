using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole<Guid>{
    public ApplicationRole(string name)
    {
        Name = name;
    }
}