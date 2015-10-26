namespace Quilt4.Api.Entities
{
    public class IssueType
    {
        public IssueType(Issue[] issues)
        {
            Issues = issues;
        }

        public Issue[] Issues { get; }
    }
}