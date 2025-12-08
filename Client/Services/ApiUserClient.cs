using Client.DTOs;
using Share.Common;

namespace Client.Services
{
    public class ApiUserClient
    {
        private readonly HttpClient _http;

        public ApiUserClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        // GET PAGE
        public async Task<PageResult<ReadUserDTO>?> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search,
            string? sortBy,
            bool desc)
            {
            var query = new List<string>
                {
                    $"pageIndex={pageIndex}",
                    $"pageSize={pageSize}",
                    $"desc={desc}"
            };

            if (!string.IsNullOrWhiteSpace(search))
                query.Add($"search={Uri.EscapeDataString(search)}");

            if (!string.IsNullOrWhiteSpace(sortBy))
                query.Add($"sortBy={sortBy}");

            var url = "api/user/paging?" + string.Join("&", query);

            return await _http.GetFromJsonAsync<PageResult<ReadUserDTO>>(url);
        }


        // GET BY ID
        public async Task<ReadUserDTO?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ReadUserDTO>($"api/user/{id}");
        }

        // CREATE
        public async Task<bool> CreateAsync(CreateUserDTO dto)
        {
            var res = await _http.PostAsJsonAsync("api/user", dto);
            return res.IsSuccessStatusCode;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(Guid id, UpdateUserDTO dto)
        {
            var res = await _http.PutAsJsonAsync($"api/user/{id}", dto);
            return res.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            var res = await _http.DeleteAsync($"api/user/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
