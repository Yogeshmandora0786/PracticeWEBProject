using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PracticeWEBProject.Dto;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace PracticeWEBProject.Repository
{
    public class LoginRepository
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public LoginRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;
        }

        // Function to call the Login API
        public async Task<CommonResponseModel> LoginAsync(LoginViewModel loginModel)
        {
            try
            {
                var serializedItemToCreate = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(serializedItemToCreate, Encoding.UTF8, "application/json");

                // Use the base URL to make the POST request
                var response = await _client.PostAsync(_baseUrl + "SignIn", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new CommonResponseModel
                    {
                        IsSuccess = false,
                        Message = "Login failed. Please check your credentials."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new CommonResponseModel
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
