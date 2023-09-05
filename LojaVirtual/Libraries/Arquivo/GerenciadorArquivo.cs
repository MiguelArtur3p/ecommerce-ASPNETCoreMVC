using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
       
        public static string CadastrarImagemProduto(IFormFile file)
        {
           
            var NomeArquivo = Path.GetFileName(file.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);
            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/uploads/temp", NomeArquivo).Replace("\\", "/");
        }
        public static bool ExcluirImagemProduto(string caminho)
        {
            
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            
            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                
                return true;
            }
            else { return false; }

        }

        public static List<Imagem> MoverImagensProduto(List<string> listCaminhoTemp, int produtoId)
        {
            //Cria a Pasta do produto
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());
            if (!Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }
            //Move a imagem para pasta definitiva!
            List<Imagem> ListaImagem = new List<Imagem>();
            foreach (var CaminhoTemp in listCaminhoTemp)
            {
                if (!string.IsNullOrEmpty(CaminhoTemp))
                {
                    var NomeArquivo = Path.GetFileName(CaminhoTemp);

                    var CaminhoDef = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/");
                    if (CaminhoDef != CaminhoTemp)
                    {
                        var caminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);
                        var caminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString(), NomeArquivo);

                        if (File.Exists(caminhoAbsolutoTemp))
                        {
                            //SE TIVER DUPLICATA DELETA O ARQUIVO DO CAMINHO DE DESTINO
                            if (File.Exists(caminhoAbsolutoDef))
                            {
                                File.Delete(caminhoAbsolutoDef);
                            }
                            File.Copy(caminhoAbsolutoTemp, caminhoAbsolutoDef);
                            if (File.Exists(caminhoAbsolutoDef))
                            {
                                File.Delete(caminhoAbsolutoTemp);
                            }
                            ListaImagem.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });

                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        ListaImagem.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });

                    }
                }

            }
            return ListaImagem;
        }

        public static void ExcluirImagensProduto(List<Imagem> imagens)
        {
            int ProdutoId=0;
            foreach(var imagem in imagens)
            {
                ExcluirImagemProduto(imagem.Caminho);
                ProdutoId = imagem.ProdutoId;
            }
            var PastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads",ProdutoId.ToString());
            if (Directory.Exists(PastaProduto))
            {
                Directory.Delete(PastaProduto);
            }
                
        }
    }
}
