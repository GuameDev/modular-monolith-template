namespace ModularMonolith.Template.SharedKernel.Pagination.Responses
{
    public class PageListResponse<TListItemResponse> where TListItemResponse : class
    {
        public IEnumerable<TListItemResponse> Elements { get; set; } = [];
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
