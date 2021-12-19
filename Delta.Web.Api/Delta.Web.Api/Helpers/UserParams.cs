namespace Delta.Web.Api
{
    public class UserParams
    {
        private int pageSize = 10;
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? CurrentUsername { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
