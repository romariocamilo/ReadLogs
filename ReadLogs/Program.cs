using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace ReadLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Digite o caminho do log: ");
                string caminho = Console.ReadLine();
                string caminhoOriginal = @caminho;

                Console.WriteLine("Digite a pasta destinho: ");
                string pasta = Console.ReadLine();
                string pastaDestino = pasta;

                if (pastaDestino.Count() < 3)
                {
                    Console.WriteLine("Caminho inválido");
                    Console.WriteLine("Tecle algo para finalizar");
                    Console.ReadKey();
                    return;
                }

                if (Char.IsLetter(pastaDestino[0]) && pastaDestino[1] == ':' && pastaDestino[2] == '\\')
                {
                    Queue<Linha> listaLinhasFiltradas = new Queue<Linha>();
                    List<string> listaDatas = new List<string>();
                    StreamReader arquivoOriginal = new StreamReader(caminhoOriginal);
                    string linha;

                    while ((linha = arquivoOriginal.ReadLine()) != null)
                    {
                        if (linha.Contains("35=n") || linha.Contains("35=") == false)
                        {
                            Linha oLinha = new Linha(linha);
                            listaLinhasFiltradas.Enqueue(oLinha);

                            if (listaDatas.Contains(oLinha.dataRegistro) == false)
                            {
                                if (oLinha.linha.Contains("35=n"))
                                {
                                    listaDatas.Add(oLinha.dataRegistro);
                                }
                            }
                        }
                    }

                    List<Arquivo> listaArquivos = new List<Arquivo>();
                    foreach (var v in listaDatas)
                    {
                        Arquivo oArquivo = new Arquivo(v, pastaDestino);
                        listaArquivos.Add(oArquivo);
                    }

                    foreach (var v in listaArquivos)
                    {
                        int contador = listaLinhasFiltradas.Count();
                        while (contador > 0)
                        {
                            if (v.nomeArquivo == listaLinhasFiltradas.First().dataRegistro)
                            {
                                v.novoArquivo.WriteLine(listaLinhasFiltradas.First().linha);
                                listaLinhasFiltradas.Dequeue();
                            }
                            else if (listaLinhasFiltradas.First().dataRegistro == "")
                            {
                                v.novoArquivo.WriteLine(listaLinhasFiltradas.First().linha);
                                listaLinhasFiltradas.Dequeue();
                            }
                            else
                            {
                                break;
                                //listaLinhasFiltradas.Enqueue(listaLinhasFiltradas.Dequeue());
                            }
                            contador--;
                        }

                    }

                    foreach (var v in listaArquivos)
                    {
                        v.novoArquivo.Close();
                    }
                    Console.WriteLine("Desmebramento de logs finalizado");
                    Console.WriteLine("Tecle algo para finalizar");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Destino Inválido");
                    Console.WriteLine("Tecle algo para finalizar");
                    Console.ReadKey();

                }
            }
            
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find file"))
                {
                    Console.WriteLine("Não foi possível encontrar o arquivo informado");
                }
                else if (ex.Message.Contains("Could not find a part of the path"))
                {
                    Console.WriteLine("Não foi possível encontrar a pasta informada");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Tecle algo para finalizar");
                Console.ReadKey();
            }
        }
    }
}
