using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PracticeWEBProject.Dto;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using PracticeWEBProjectWEB.Dto;

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
                var serializedItemToCreate = JsonConvert.SerializeObject(loginModel.Username);
                var content = new StringContent(serializedItemToCreate, Encoding.UTF8, "application/json");

                // Construct the URL with query parameters
                string url = $"{_baseUrl}api/Login/Login_Active_Inactive?UserName={loginModel.Username}&Password={loginModel.Password}";

                // Use the base URL to make the POST request
                var response = await _client.PostAsync(url, null); // Assuming no request body


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsondata = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                    return jsondata;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new CommonResponseModel
                    {
                        ResponseCode = 0, // Adjust according to your application's logic (e.g., failure status code)
                        ResponseMessage = "Login failed. Please check your credentials."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new CommonResponseModel
                {
                    ResponseCode = 0, // Adjust failure code
                    ResponseMessage = $"An error occurred: {ex.Message}"
                };
            }

        }
    }
}
