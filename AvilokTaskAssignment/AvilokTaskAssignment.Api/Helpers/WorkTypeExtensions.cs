using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Helpers
{
    public static class WorkTypeExtensions
    {
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

        public static string GetLeaderRoleName (this WorkType workType)
        {
            return $"Leader {workType.GetRoleName()}";
        }
    }
}
