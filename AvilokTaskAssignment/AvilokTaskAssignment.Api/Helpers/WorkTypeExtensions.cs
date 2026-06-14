using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Helpers
{
    public static class WorkTypeExtensions
    {
        /// <summary>
        /// Vrátí textový název role pro danou hodnotu <see cref="WorkType"/>.
        /// </summary>
        public static string GetRoleName(this WorkType workType)
        {
            return workType switch
            {
                WorkType.Developer => "Developer",
                WorkType.GraphicDesigner => "Graphic",
                WorkType.Storyteller => "Story",
                _ => throw new Exception("Neznámí typ práce.")
            };
        }
        /// <summary>
        /// Vrátí text názvu role pro vedoucí pozici ve formátu "Leader {Role}".
        /// </summary>
        public static string GetLeaderRoleName (this WorkType workType)
        {
            return $"Leader {workType.GetRoleName()}";
        }
    }
}
