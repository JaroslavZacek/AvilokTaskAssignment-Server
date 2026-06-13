using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Helpers
{
    public static class PermissionHelper
    {
        public static bool HasTaskLeaderAccess(IEnumerable<string> roles, WorkType workType)
        {
            return roles.Contains("Admin") || roles.Contains(workType.GetLeaderRoleName());
        }

        public static void EnsureTaskLeaderAccess(IEnumerable<string> roles, WorkType workType, string errorMessage)
        {
            if (!HasTaskLeaderAccess(roles, workType))
                throw new Exception(errorMessage);
        }
    }
}
