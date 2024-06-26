using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;

        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                                                  ");
            Console.WriteLine("            Digite a opção desejada:              ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("            1 - Criar Conta                       ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("            2 - Entrar com CPF e Senha            ");
            Console.WriteLine("        ================================          ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaDeLogin();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                  ");
            Console.WriteLine("               Digite seu nome:                   ");
            string nome = Console.ReadLine();
            Console.WriteLine("        ================================          ");
            Console.WriteLine("               Digite o CPF:                      ");
            string cpf = Console.ReadLine();
            Console.WriteLine("        ================================          ");
            Console.WriteLine("               Digite sua senha:                  ");
            string senha = Console.ReadLine();
            Console.WriteLine("        ================================          ");

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("       Conta cadastrada com sucesso.              ");
            Console.WriteLine("    ====================================          ");

            Thread.Sleep(1000);

            TelaContaLogada(pessoa);
        }

        private static void TelaDeLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                  ");
            Console.WriteLine("                Digite o CPF:                     ");
            string cpf = Console.ReadLine();
            Console.WriteLine("        ================================          ");
            Console.WriteLine("                Digite sua senha:                 ");
            string senha = Console.ReadLine();
            Console.WriteLine("        ================================          ");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("           Pessoa não cadastrada                  ");
                Console.WriteLine("    ====================================          ");
            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"Nome: {pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()}" + 
                $"| Agência {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()}";

            Console.WriteLine("");
            Console.WriteLine($"              Seja bem vindo, {msgTelaBemVindo}               ");
            Console.WriteLine("");
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                  ");
            Console.WriteLine("             Digite a opção desejada              ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("             1 - Realizar um Depósito             ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("             2 - Realizar um Saque                ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("             3 - Consultar Saldo                  ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("             4 - Extrato                          ");
            Console.WriteLine("        ================================          ");
            Console.WriteLine("             5 - Sair                             ");
            Console.WriteLine("        ================================          ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    Thread.Sleep(1000);
                    OpcaoVoltarLogado(pessoa);
                    break;

                case 2:
                    TelaSaque(pessoa);
                    Thread.Sleep(1000);
                    OpcaoVoltarLogado(pessoa);
                    break;

                case 3:
                    TelaConsultaSaldo(pessoa);
                    break;

                case 4:
                    TelaExtrato(pessoa);
                    break;

                case 5:
                    TelaPrincipal();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("                  Opcao invalida                  ");
                    Console.WriteLine("        ================================          ");
                    break;
            }

        }
        
        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Digite o valor do deposito:                      ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("       ===========================================          ");

            Thread.Sleep(1000);
            Console.Clear();

            pessoa.Conta.Deposita(valor);

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                ");
            Console.WriteLine("                                                                ");
            Console.WriteLine("                Deposito realizado com sucesso                  ");
            Console.WriteLine("          ============================================          ");
            Console.WriteLine("                                                                ");
        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Digite o valor do saque:                      ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("       ===========================================          ");

            Thread.Sleep(1000);
            Console.Clear();

            pessoa.Conta.Saca(valor);

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                ");
            Console.WriteLine("                                                                ");
            Console.WriteLine("                Saque realizado com sucesso                     ");
            Console.WriteLine("          ============================================          ");
            Console.WriteLine("                                                                ");
        }
        private static void TelaConsultaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                ");
            Console.WriteLine($"               Seu Saldo: ${pessoa.Conta.ConsultaSaldo()}      ");
            Console.WriteLine("         ==========================================             ");
            Console.WriteLine("                                                                ");
            Console.WriteLine("        Digite 1 para voltar para as opcoes                     ");
            Console.WriteLine("        ==========================================              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                OpcaoVoltarLogado(pessoa);
            }
            else
            {
                Console.WriteLine("                    Opcao Invalida                      ");
                Console.WriteLine("         =======================================        ");
            }
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any()) 
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                                ");
                    Console.WriteLine($"         Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}  ");
                    Console.WriteLine($"         Tipo de movimentacao: {extrato.Descricao}             ");
                    Console.WriteLine($"         Valor: {extrato.Valor}                                ");
                    Console.WriteLine("         ==========================================             ");
                }

                Console.WriteLine("                                                                ");
                Console.WriteLine("                                                                ");
                Console.WriteLine($"                    Sub Total: {total}                         ");
                Console.WriteLine("         ==========================================             ");
            }
            else
            {
                Console.WriteLine("             Nao ha extrato a ser exibido               ");
                Console.WriteLine("         =======================================        ");
            }

            Console.WriteLine("        Digite 1 para voltar para as opcoes                     ");
            Console.WriteLine("        ==========================================              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                OpcaoVoltarLogado(pessoa);
            }
            else
            {
                Console.WriteLine("                    Opcao Invalida                      ");
                Console.WriteLine("         =======================================        ");
            }
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.Clear();

            Console.WriteLine("              Entre com uma opcao abaixo:                       ");
            Console.WriteLine("         =======================================                ");
            Console.WriteLine("              1 - Voltar para minha conta                       ");
            Console.WriteLine("         =======================================                ");
            Console.WriteLine("              2 - Sair                                          ");
            Console.WriteLine("         =======================================                ");

            opcao = int.Parse(Console.ReadLine());

            if(opcao == 1)
            {
                TelaContaLogada(pessoa);
            } else
            {
                TelaPrincipal();
            }
        }

        private static void OpcaoVoltarDeslogado()
        {
            Console.WriteLine("              Entre com uma opcao abaixo:                       ");
            Console.WriteLine("         =======================================                ");
            Console.WriteLine("              1 - Voltar para o menun principal                 ");
            Console.WriteLine("         =======================================                ");
            Console.WriteLine("              2 - Sair                                          ");
            Console.WriteLine("         =======================================                ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                TelaPrincipal();
            }
            else
            {
                Console.WriteLine("                    Opcao Invalida                      ");
                Console.WriteLine("         =======================================        ");
            } 
        }      
    } 
}
