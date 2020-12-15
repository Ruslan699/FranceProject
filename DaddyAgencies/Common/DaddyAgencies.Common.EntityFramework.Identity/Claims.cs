
namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public static  class Claims
    {
        public static string Uid => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uid";
        public static string FullName => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fullname";
        public static string PhoneNumber => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/phonenumber";
    }
}