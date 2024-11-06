using Microsoft.AspNetCore.Authentication;
using PDFLines.Data;
using PDFLines.Models;
using System.Security.Claims;

namespace PDFLines
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly TCZNT5000 _context;
        private readonly TCZNT58 _context58;

        public ClaimsTransformer(TCZNT5000 context, TCZNT58 context58)
        {
            _context = context;
            _context58 = context58;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            string userName = identity.Name.ToString().Substring(7);

            var user = _context.Users
                .FirstOrDefault(t => t.Alias == userName);
            if (user == null)
            {
                TryGetUserFullName(userName, out Guid employeeId, out string name, out string surname, out string email);

                var userAccess = "noaccess";
                if (userName.Substring(0, 3) == "tcz")
                {
                    userAccess = "user";
                }

                user = new User
                {
                    EmployeeId = employeeId,
                    Alias = userName,
                    NameSurname = name + " " + surname,
                    Email = email.ToLower(),
                    AccessLevel = userAccess
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            var claim = new Claim(identity.RoleClaimType, user.AccessLevel);
            identity.AddClaim(claim);
            return Task.FromResult(principal);
        }

        private void TryGetUserFullName(string user, out Guid employeeId, out string name, out string surname, out string email)
        {
            try
            {
                var userAd = _context58.Employees?
                    .FirstOrDefault(t => t.Alias == user);
                employeeId = userAd.Id;
                name = userAd.FirstName;
                surname = userAd.LastName;
                email = userAd.Email;
            }

            catch (Exception e)
            {
                employeeId = Guid.Empty;
                name = "-";
                surname = "-";
                email = "-";
            }
        }
    }
}
