using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization
{

    public class HrManagerProbationRequerement : IAuthorizationRequirement
    {
        public int ProbationMonths { get; set; }
        public HrManagerProbationRequerement(int probationMonth)
        {
            ProbationMonths = probationMonth;
        }

    }

    public class HrManagerProbationRequerementHandler : AuthorizationHandler<HrManagerProbationRequerement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HrManagerProbationRequerement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "EmploymentDate"))
                return Task.CompletedTask;

            if (DateTime.TryParse(context.User.FindFirst(x => x.Type == "EmploymentDate")?.Value, out DateTime employmentDate))
            {
                var period = DateTime.Now - employmentDate;
                if (period.Days > 30 * requirement.ProbationMonths)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }




}
