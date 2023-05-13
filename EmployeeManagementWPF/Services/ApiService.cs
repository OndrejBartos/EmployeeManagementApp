using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagementWPF.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private string? _token;

    public bool IsLoggedIn => _token is not null;

    private class LoginResponse
    {
        public string Token { get; set; } = null!;
    }

    public ApiService(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    public async Task<bool> Login(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/identity", new
        {
            username = username,
            password = password
        });

        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
        _token = content?.Token;

        return _token is not null;
    }
}
