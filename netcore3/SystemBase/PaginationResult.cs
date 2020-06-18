namespace SystemBase
{
    public class PaginationResult
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public int TotalCount { get; set; }
    }
}
