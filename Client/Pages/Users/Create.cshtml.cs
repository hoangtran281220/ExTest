using Client.DTOs;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ApiUserClient _api;

        public CreateModel(ApiUserClient api)
        {
            _api = api;
        }

        [BindProperty]
        public CreateUserDTO Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            bool ok = await _api.CreateAsync(Input);

            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create user");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
