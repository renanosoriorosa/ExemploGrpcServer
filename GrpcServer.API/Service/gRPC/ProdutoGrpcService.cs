
using Grpc.Core;
using GrpcServer.API.Models;

namespace GrpcServer.API.Service.gRPC
{
    public class ProdutoGrpcService : ProdutoControllerGRPC.ProdutoControllerGRPCBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoGrpcService(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public override async Task<ListObterProdutosResponse> ObterProdutos(ObterProdutosRequest request, ServerCallContext context)
        {
            var produtos = _produtoService.ObterTodosProdutos();

            return MapProdutosToProtoResponse(produtos);
        }

        public override async Task<ObterProdutosResponse> ObterProdutoPorId(ObterProdutoPorIdRequest request, ServerCallContext context)
        {
            var produto = _produtoService.ObterProdutoPorId(int.Parse(request.Id));

            return MapProdutoToProtoResponse(produto);
        }

        private ObterProdutosResponse MapProdutoToProtoResponse(Produto produto)
        {
            if(produto is null)
                return new ObterProdutosResponse();

            return new ObterProdutosResponse
            {
                Id = produto.Id.ToString(),
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco,
            };
        }

        private ListObterProdutosResponse MapProdutosToProtoResponse(List<Produto> produtos)
        {   
            List<ObterProdutosResponse> ObterProdutosResponse = new List<ObterProdutosResponse>();

            foreach (var produto in produtos)
                ObterProdutosResponse.Add(MapProdutoToProtoResponse(produto));

            return new ListObterProdutosResponse { Produtos = { ObterProdutosResponse } };
        }
    }
}
