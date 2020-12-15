using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.Util;

namespace WebApp.Helpers
{

    public static class ResultExtensions
    {

        private static Dictionary<IssueOrigin, IEnumerable<string>> MapIssueToErrorResponse(Issue issue) =>
            new Dictionary<IssueOrigin, IEnumerable<string>>
            {
                [issue.Origin] = issue.Reasons
            };
    }
}