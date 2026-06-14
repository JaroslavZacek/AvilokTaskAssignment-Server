using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Helpers
{
    public static class PermissionHelper
    {
        /// <summary>
        /// Zkontroluje, zda sada rolí obsahuje roly Admin nebo Leader.
        /// </summary>
        public static bool HasTaskLeaderAccess(IEnumerable<string> roles, WorkType workType)
        {
            return roles.Contains("Admin") || roles.Contains(workType.GetLeaderRoleName());
        }

        /// <summary>
        /// Zajistí, že uživatel má oprávnění vedoucího pro zadaný typ práce.
        /// </summary>
        public static void EnsureTaskLeaderAccess(IEnumerable<string> roles, WorkType workType, string errorMessage)
        {
            if (!HasTaskLeaderAccess(roles, workType))
                throw new Exception(errorMessage);
        }
    }
}
