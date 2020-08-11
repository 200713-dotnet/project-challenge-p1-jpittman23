using System.Linq;
using PizzaStore.Client.Models;

namespace PizzaStore.Client.Models
{
    public enum UserRole
    {
        RegularUser,
        PreimumUser,
        Admin
    }
}

namespace Microsoft.AspNetCore.Authorization
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(roles=>roles.ToString()));
        }
    }
}