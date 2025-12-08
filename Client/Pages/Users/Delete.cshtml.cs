using Client.DTOs;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly ApiUserClient _api;

        public DeleteModel(ApiUserClient api)
        {
            _api = api;
        }

        public ReadUserDTO? UserData { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserData = await _api.GetByIdAsync(Id);

            if (UserData == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ok = await _api.DeleteAsync(Id);

            return RedirectToPage("Index");
        }
    }
}
