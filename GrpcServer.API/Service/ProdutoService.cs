using GrpcServer.API.Models;
using System.Collections.Generic;

namespace GrpcServer.API.Service
{
    public class ProdutoService
    {
        private List<Produto> _produtos => PopulaProdutos();

        public List<Produto> ObterTodosProdutos()
        {
            return _produtos;
        }

        public Produto? ObterProdutoPorId(int idProduto)
        {
            return _produtos
                .FirstOrDefault(x => x.Id == idProduto);
        }

        private List<Produto> PopulaProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            for (int i = 1; i <= 10; i++)
            {
                Produto produto = new Produto
                {
                    Id = i,
                    Codigo = $"COD{i}",
                    Nome = $"Produto {i}",
                    Preco = 10.0 * i
                };

                produtos.Add(produto);
            }

            return produtos;
        }
    }
}
