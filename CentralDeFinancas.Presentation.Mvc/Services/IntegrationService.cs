using System.Text;

namespace CentralDeFinancas.Presentation.Mvc.Services
{
    public class IntegrationService
    {
        private readonly string? _apiUrl;

        public IntegrationService(string? apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<string> PostAsync(string endpoint, StringContent content)
        {
            using (var HttpClient = new HttpClient())
            {
                // Requisição POST para a API
                var result = await HttpClient.PostAsync(_apiUrl + endpoint, content);

                // Lendo a resposta da requisição
                var builder = new StringBuilder();
                using (var response = result.Content)
                {
                    Task<string> task = response.ReadAsStringAsync();
                    builder.Append(task.Result);
                }

                var jsonResult = builder.ToString();

                // Verificando se a resposta é sucesso
                if (result.IsSuccessStatusCode)
                    return jsonResult;
                else
                    throw new Exception(jsonResult);
            }
        }
    }
}
