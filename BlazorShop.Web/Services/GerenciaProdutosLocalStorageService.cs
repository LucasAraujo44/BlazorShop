using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;
using System.Runtime.CompilerServices;

namespace BlazorShop.Web.Services
{
	public class GerenciaProdutosLocalStorageService : IGerenciaProdutosLocalStorageService
	{
		private const string key = "ProdutoCollection";

		private readonly ILocalStorageService localStorageService;
		private readonly IProdutoService produtoService;
		
		public GerenciaProdutosLocalStorageService(ILocalStorageService localStorageService, IProdutoService produtoService)
		{
			this.localStorageService = localStorageService;
			this.produtoService = produtoService;
		}

		public async Task<IEnumerable<ProdutoDto>> GetCollection()
		{
			return await this.localStorageService.GetItemAsync<IEnumerable<ProdutoDto>>(key) ?? await AddColletion();
		}

		public  async Task RemoveCollection()
		{
			await this.localStorageService.RemoveItemAsync(key);
		}

		private async Task<IEnumerable<ProdutoDto>> AddColletion() 
		{
			var produtoColletion = await this.produtoService.GetItens();
			if(produtoColletion != null) 
			{
				await this.localStorageService.SetItemAsync(key, produtoColletion);
			}
			return produtoColletion;
		}
	}
}
