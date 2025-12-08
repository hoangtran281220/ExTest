using Client.DTOs;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApiUserClient _api;

        public EditModel(ApiUserClient api)
        {
            _api = api;
        }

        [BindProperty]
        public UpdateUserDTO Input { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _api.GetByIdAsync(Id);

            if (user == null)
                return RedirectToPage("Index");

            Input = new UpdateUserDTO
            {
                Code = user.Code,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                Address = user.Address
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var ok = await _api.UpdateAsync(Id, Input);

            if (!ok)
            {
                ModelState.AddModelError("", "Update failed");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
