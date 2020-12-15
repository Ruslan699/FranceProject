
namespace DaddyAgencies.Common.Contracts
{
    public enum IssueOrigin
    {
        UserInput = 1,
        Controversy = 2,
        ExternalFailure = 3,
        ExternalTimeout = 4,
        Error = 5,
        Unknown = 6,
    }
}
