using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PracticeWEBProject.Dto;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using PracticeWEBProjectWEB.Dto;

namespace PracticeWEBProject.Repository
{
    public class RegisterRepository
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public RegisterRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;
        }

        // Function to call the Registration API
        public async Task<CommonResponseModel> RegisterAsync(RegisterViewModel registerModel)
        {
            try
            {
                // Serialize the registration model into a JSON string
                var serializedItemToCreate = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(serializedItemToCreate, Encoding.UTF8, "application/json");

                // Construct the URL for the registration API
                string url = $"{_baseUrl}api/Registration/Registration_Upsert";

                // Use the base URL to make the POST request
                var response = await _client.PostAsync(url, content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsondata = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                    return jsondata;  // Return the response model from the API
                }
                else
                {
                    // Log the error and return a failure message
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new CommonResponseModel
                    {
                        ResponseCode = 0,  // Adjust according to your application's failure logic
                        ResponseMessage = "Registration failed. Please try again."
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure message
                Console.WriteLine($"Exception: {ex.Message}");
                return new CommonResponseModel
                {
                    ResponseCode = 0,  // Adjust failure code
                    ResponseMessage = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
