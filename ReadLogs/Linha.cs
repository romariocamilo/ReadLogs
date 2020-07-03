using System;
using System.Collections.Generic;
using System.Text;

namespace ReadLogs
{
    public class Linha
    {
        public string linha { get; set; }
        public string dataRegistro { get; set; }
        
        public Linha(string linha)
        {
            try
            {
                this.linha = linha;
                dataRegistro = RetornaLinha(linha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public string RetornaLinha(string linha)
        {
            int contador = 0;
            string data = "";

            foreach (var v in linha)
            {
                if (v == '5' && linha[contador + 1] == '2' && linha[contador + 2] == '=')
                {
                    data = linha.Substring(contador + 3, 8);
                    contador = 0;

                    break;
                }
                contador++;
            }
            return data;
        }
    }
}
