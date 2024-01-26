using ClientGrpc.Dtos;
using GrpcServer.API.Service.gRPC;

namespace ClientGrpc.Service.gRPC
{
    public interface IProdutoGrpcService
    {
        Task<List<ProdutoGrpcDTO>> ObterProdutos();
        Task<ProdutoGrpcDTO> ObterProdutoPorIdAsync(int idProduto);
    }

    public class ProdutoGrpcService : IProdutoGrpcService
    {
        private readonly ProdutoControllerGRPC.ProdutoControllerGRPCClient _produtoControllerGRPC;

        public ProdutoGrpcService(ProdutoControllerGRPC.ProdutoControllerGRPCClient produtoControllerGRPC)
        {
            _produtoControllerGRPC = produtoControllerGRPC;
        }

        public async Task<List<ProdutoGrpcDTO>> ObterProdutos()
        {
            var produtos = await _produtoControllerGRPC.ObterProdutosAsync(new ObterProdutosRequest());

            return MapProdutosToProtoResponse(produtos);
        }

        public async Task<ProdutoGrpcDTO?> ObterProdutoPorIdAsync(int idProduto)
        {
            var produto = await _produtoControllerGRPC
                .ObterProdutoPorIdAsync(new ObterProdutoPorIdRequest { Id = idProduto.ToString() });

            return MapProdutoToProtoResponse(produto);
        }

        private ProdutoGrpcDTO? MapProdutoToProtoResponse(ObterProdutosResponse produto)
        {
            if (produto is null)
                return null;

            if (string.IsNullOrEmpty(produto.Id))
                return null;

            return new ProdutoGrpcDTO
            {
                Id = int.Parse(produto.Id),
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco,
            };
        }

        private List<ProdutoGrpcDTO> MapProdutosToProtoResponse(ListObterProdutosResponse produtosResponse)
        {
            List<ProdutoGrpcDTO> ProdutosGrpcDTO = new List<ProdutoGrpcDTO>();

            foreach (var produto in produtosResponse.Produtos)
                ProdutosGrpcDTO.Add(MapProdutoToProtoResponse(produto)!);

            return ProdutosGrpcDTO;
        }
    }
}
