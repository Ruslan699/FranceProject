using System;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using DaddyAgencies.Common.EntityFramework.Identity;
using Microsoft.AspNet.Identity;

namespace WebApp.Helpers
{
    public static class StreamExtension
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            byte[] data;
            using (var br = new BinaryReader(stream))
            {
                data = br.ReadBytes((int)stream.Length);
            }
            return data;
        }
    }
    public static class UserExtensions
    {
        public static string GetValue(this IPrincipal user, string claim)
            => ((ClaimsIdentity)user.Identity).FindFirstValue(claim);

        public static Guid GetGuidValue(this IPrincipal user, string claim)
            => Guid.Parse(GetValue(user, claim));

        public static Guid GetUid(this IPrincipal user)
            => GetGuidValue(user, ClaimTypes.NameIdentifier);

        public static string GetEmail(this IPrincipal user)
            => GetValue(user, ClaimTypes.Name);

        public static string GetFullName(this IPrincipal user)
            => GetValue(user, Claims.FullName);

        public static string GetPhoneNumber(this IPrincipal user)
            => GetValue(user, Claims.PhoneNumber);
    }
}