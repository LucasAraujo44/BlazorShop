using BlazorShop.Models.DTOs;
using System.Threading.Tasks;

namespace BlazorShop.Web.Services
{
    public interface ICarrinhoCompraService
    {
        Task<List<CarrinhoItemDto>> GetItens(string ususarioId);
        Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
       Task<CarrinhoItemDto> DeletaItem(int id);

    }
}
