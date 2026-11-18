namespace GerenciadorTarefas
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Lista com todas as tarefas
            List<Tarefa> tarefas = new List<Tarefa>();

            // Pilha com os ids das tarefas concluidas (a ultima conclusao fica no topo)
            Stack<int> concluidas = new Stack<int>();

            int proximoId = 1;
            bool run = true;

            do
            {
                Console.WriteLine("");
                Console.WriteLine("GERENCIADOR DE TAREFAS");
                Console.WriteLine("1 - Cadastrar tarefa");
                Console.WriteLine("2 - Listar tarefas");
                Console.WriteLine("3 - Buscar tarefa");
                Console.WriteLine("4 - Concluir tarefa");
                Console.WriteLine("5 - Desfazer a última conclusão");
                Console.WriteLine("6 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine() + "";
                Console.WriteLine("");

                switch (opcao)
                {
                    case "1":
                    {
                        Console.Write("Descrição: ");
                        Tarefa tarefa = new Tarefa();
                        tarefa.Id = proximoId;
                        tarefa.Descricao = Console.ReadLine() + "";
                        tarefa.Concluida = false;
                        tarefas.Add(tarefa);
                        proximoId++;
                        Console.WriteLine($"Tarefa cadastrada com o id {tarefa.Id}.");
                        break;
                    }

                    case "2":
                    {
                        if (tarefas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma tarefa cadastrada.");
                            break;
                        }

                        foreach (Tarefa tarefa in tarefas)
                        {
                            string situacao = tarefa.Concluida ? "Concluída" : "Pendente";
                            Console.WriteLine($"{tarefa.Id} - {tarefa.Descricao} - {situacao}");
                        }
                        break;
                    }

                    case "3":
                    {
                        if (tarefas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma tarefa cadastrada.");
                            break;
                        }

                        Console.Write("Id da tarefa: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Id inválido. Digite um número inteiro.");
                            break;
                        }

                        Tarefa? tarefa = tarefas.Find(t => t.Id == id);
                        if (tarefa == null)
                        {
                            Console.WriteLine("Nenhuma tarefa encontrada com esse id.");
                            break;
                        }

                        string situacao = tarefa.Concluida ? "Concluída" : "Pendente";
                        Console.WriteLine($"{tarefa.Id} - {tarefa.Descricao} - {situacao}");
                        break;
                    }

                    case "4":
                    {
                        if (tarefas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma tarefa cadastrada.");
                            break;
                        }

                        Console.Write("Id da tarefa: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Id inválido. Digite um número inteiro.");
                            break;
                        }

                        Tarefa? tarefa = tarefas.Find(t => t.Id == id);
                        if (tarefa == null)
                        {
                            Console.WriteLine("Nenhuma tarefa encontrada com esse id.");
                            break;
                        }

                        if (tarefa.Concluida)
                        {
                            Console.WriteLine("Essa tarefa já está concluída.");
                            break;
                        }

                        tarefa.Concluida = true;
                        concluidas.Push(tarefa.Id);
                        Console.WriteLine("Tarefa concluída.");
                        break;
                    }

                    case "5":
                    {
                        if (concluidas.Count == 0)
                        {
                            Console.WriteLine("Não há conclusão para desfazer.");
                            break;
                        }

                        // Tira o id do topo da pilha e volta a tarefa para pendente
                        int id = concluidas.Pop();
                        Tarefa tarefa = tarefas.Find(t => t.Id == id)!;
                        tarefa.Concluida = false;
                        Console.WriteLine($"A tarefa {id} voltou para pendente.");
                        break;
                    }

                    case "6":
                        run = false;
                        Console.WriteLine("Programa encerrado");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (run);
        }
    }
}
