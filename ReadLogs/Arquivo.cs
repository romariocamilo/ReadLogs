using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReadLogs
{
    public class Arquivo
    {
        public string nomeArquivo { get; set; }
        public string caminho { get; set; }
        public StreamWriter novoArquivo { get; set; }
        
        public Arquivo(string data, string pastaDestino)
        {
            if (Directory.Exists(pastaDestino))
            {
                nomeArquivo = data;
                caminho = pastaDestino + "\\" + data + ".log";
                novoArquivo = new StreamWriter(caminho);
            }
            else
            {
                Directory.CreateDirectory(pastaDestino);
                nomeArquivo = data;
                caminho = @pastaDestino + "\\" + data + ".log";
                novoArquivo = new StreamWriter(caminho);
            }
        }
    }
}
