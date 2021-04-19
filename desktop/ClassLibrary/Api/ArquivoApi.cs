using ClassLibrary.Models;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Api
{
    public class ArquivoApi
    {
        RestClient Client = new RestClient("https://cloud-txt.herokuapp.com/");

        public async Task<IEnumerable<Arquivo>> GetArquivos()
        {
            var request = new RestRequest("api", DataFormat.Json);
            return await Client.GetAsync<IEnumerable<Arquivo>>(request);
        }

        public async Task DeleteArquivo(Arquivo arquivo)
        {
            if (arquivo is null) return;

            var request = new RestRequest($"api/{arquivo.Id}", DataFormat.Json);
            await Client.DeleteAsync<Arquivo>(request);            
        }

        public async Task<Arquivo> SaveArquivo(Arquivo arquivo)
        {
            if (arquivo is null) return null;

            //var arquivoJson = JsonSerializer.Serialize(arquivo);
            var request = new RestRequest("api").AddJsonBody(arquivo);
            return await Client.PostAsync<Arquivo>(request);
        }
    }
}
