namespace GitHubTask.Queries
{
    public class PaginationQuery
    {
        public int PerPage { get; set; } = 30;
        public int Page { get; set; } = 1;

        public override string ToString()
        {
            return $"?per_page={PerPage}&page={Page}";
        }
    }
}
