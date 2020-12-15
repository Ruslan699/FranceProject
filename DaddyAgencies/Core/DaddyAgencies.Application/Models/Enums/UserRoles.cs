using DaddyAgencies.Common.MediatR.CustomAttributes;

namespace DaddyAgencies.Application.Models.Enums
{
    public enum UserRoles
    {
        [ModelUid("d390e58c-debe-4435-8ced-34715472dd3a")]
        Admin,
        [ModelUid("18124089-656f-4098-a46b-84db6afe378d")]
        Agent,
        [ModelUid("a49d68e1-97bc-496c-aaa2-4ad9c2483b18")]
        User
    }
}
