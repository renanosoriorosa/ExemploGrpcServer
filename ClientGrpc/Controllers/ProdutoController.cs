﻿using ClientGrpc.Service.gRPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace ClientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoGrpcService _produtoServiceGrpc;

        public ProdutoController(IProdutoGrpcService produtoServiceGrpc)
        {
            _produtoServiceGrpc = produtoServiceGrpc;
        }

        [HttpGet]
        public async Task<IActionResult> ObterProdutos()
        {
            try
            {
                return Ok(await _produtoServiceGrpc.ObterProdutos());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterProdutoPorId(int idProduto)
        {
            try
            {
                return Ok(await _produtoServiceGrpc.ObterProdutoPorIdAsync(idProduto));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
