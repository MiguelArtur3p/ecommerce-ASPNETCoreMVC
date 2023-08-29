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

        public static List<string> MoverImagensProduto(List<string> listCaminhoTemp, string produtoId)
        {
            //Cria a Pasta do produto
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId);
            if (!Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }
            //Move a imagem para pasta definitiva!
            List<string> ListaCaminhoDef = new List<string>();
            foreach (var imagemTemp in listCaminhoTemp)
            {
                if (string.IsNullOrEmpty(imagemTemp))
                {


                    var NomeArquivo = Path.GetFileName(imagemTemp);
                    var caminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagemTemp);
                    var caminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId, NomeArquivo);

                    if (File.Exists(caminhoAbsolutoTemp))
                    {
                        File.Copy(caminhoAbsolutoTemp, caminhoAbsolutoDef);
                        if (File.Exists(caminhoAbsolutoDef))
                        {
                            File.Delete(caminhoAbsolutoTemp);
                        }
                        ListaCaminhoDef.Add(Path.Combine("/uploads", produtoId, NomeArquivo).Replace("\\", "/"));
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            return ListaCaminhoDef;
        }
    }
}
