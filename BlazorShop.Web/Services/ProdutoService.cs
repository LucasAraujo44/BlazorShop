using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        public HttpClient _httpCliente;
        public ILogger<ProdutoService> _logger;

        public ProdutoService(HttpClient httpCliente,
            ILogger<ProdutoService> logger)
        {
            _httpCliente = httpCliente;
            _logger = logger;
        }
        public async Task<IEnumerable<ProdutoDto>> GetItens() 
        {
            try 
            {
                var produtosDto = await _httpCliente.GetFromJsonAsync<IEnumerable<ProdutoDto>>("api/produtos");
                return produtosDto;
            }
            catch (Exception)
            {
                _logger.LogError("Erro ao acessar produtos : api/produtos ");
                throw;
            }
            
        }
		public async Task<ProdutoDto> GetItem(int id)
        {
            try
            {
                var response = await _httpCliente.GetAsync($"api/produtos/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
						return default(ProdutoDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProdutoDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Erro ao obter o produto pelo id= {id} - {message}");
                    throw new Exception($"Status Code : {response.StatusCode} - {message}");
                }
            }
            catch(Exception)
            {
				_logger.LogError($"Erro ao obter o produto pelo id= {id}");
				throw;
            }
        }

        public async Task<IEnumerable<CategoriaDto>> GetCategorias()
        {
            try 
            {
                var response = await _httpCliente.GetAsync($"api/Produtos/GetCategorias");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode ==  System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CategoriaDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CategoriaDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"http Status Code : {response.StatusCode} - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  async Task<IEnumerable<ProdutoDto>> GetItensPorCategoria(int categotiaId)
        {
            try 
            {
                var response = await _httpCliente.GetAsync($"api/Produtos/{categotiaId}/GetItensPorCategoria");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProdutoDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProdutoDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"http Status Code : {response.StatusCode} - {message}");
                }
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
