using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Share.Common;
using Client.DTOs;
namespace Client.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApiUserClient _api;

        public IndexModel(ApiUserClient api)
        {
            _api = api;
        }

        public PageResult<ReadUserDTO>? Data { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; } = "fullname";

        [BindProperty(SupportsGet = true)]
        public bool Desc { get; set; }

        public async Task OnGet()
        {
            // VALIDATE sortBy
            var allowedSorts = new[] { "code", "fullname", "email" };
            if (!allowedSorts.Contains(SortBy?.ToLower()))
                SortBy = "fullname";

            Data = await _api.GetPagedAsync(
                PageIndex,
                PageSize,
                Search,
                SortBy!,
                Desc
            );
        }
    }
}
