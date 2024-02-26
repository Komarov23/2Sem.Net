using System.Net;
using System.Text.Json;

namespace ConsoleApp5
{
    internal class Api
    {
        public class ApiResponse<T>
        {
            public required string Message { get; set; }
            public HttpStatusCode HttpStatusCode {  get; set; }
            public List<T>? Data { get; set; }
        }

        private static readonly HttpClient client = new();

        public static async Task<ApiResponse<T>> Get<T> (string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync (url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync ();
                List<T>? data = JsonSerializer.Deserialize<List<T>?>(responseBody);

                return new ApiResponse<T>
                {
                    Message = "Success",
                    HttpStatusCode = response.StatusCode,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse<T> { 
                    Message = ex.Message,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }

        public static async Task<ApiResponse<T>> Post<T>(string url, object data)
        {
            try
            {
                string requestData = JsonSerializer.Serialize(data);
                HttpContent content = new StringContent(requestData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                T result = JsonSerializer.Deserialize<T>(responseBody);

                return new ApiResponse<T>
                {
                    Message = "Success",
                    HttpStatusCode = response.StatusCode,
                    Data = new List<T> { result }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse<T>
                {
                    Message = ex.Message,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }
    }
}
