namespace EasyBank
{
    public class Transfer
    {
        public double ValorEmConta { get; set; } = 100.00;

        public double Transferir(double valorInformado)
        {
            valorInformado = ValorEmConta;
            Console.WriteLine("Informe o valor que você gostaria de transferir");
            Console.WriteLine($"valor em conta: {ValorEmConta}");
            valorInformado = Convert.ToDouble(Console.ReadLine());
            valorInformado = VerificarQuantidadeEmConta(valorInformado);
            var VerificarChavePix = VerificarPix();
            MostrarChavePix(VerificarChavePix);
            MostrarValorPix(valorInformado);

            Console.WriteLine("Digite 1 para transferir ou 2 para voltar ao menu");
            string escolha = Console.ReadLine();

            if (escolha == "1")
            {
                Console.WriteLine("Transferencia realizada.");
                ValorEmConta = ValorEmConta - valorInformado;
            }
            
            else
            {
                while (escolha != "1" && escolha != "2")
                {
                    Console.WriteLine("Escolha uma das opções acima");
                    escolha = Console.ReadLine();
                }
            }

            Console.WriteLine($"Você possui {ValorEmConta} em conta");
            return valorInformado;
        }
        public double VerificarQuantidadeEmConta(double quantidadeEmConta)
        {
            while (quantidadeEmConta > ValorEmConta)
            {
                Console.WriteLine("Valor maior que o disponível em conta, favor informe um valor válido");
                quantidadeEmConta = Convert.ToDouble(Console.ReadLine());
            }
            while (quantidadeEmConta <= 0)
            {
                Console.WriteLine("Favor informe um valor válido");
                quantidadeEmConta = Convert.ToDouble(Console.ReadLine());
            }
            return quantidadeEmConta;
        }

        public string VerificarPix()
        {
            bool pixCorreto = false;
            Console.WriteLine("Digite o pix que gostaria de transferir (E-MAIL, CPF/CNPJ OU TELEFONE)");
            string pix = Console.ReadLine();

            while (pixCorreto != true)
            {
                if (pix.Contains("@"))
                {
                    Console.WriteLine("E-mail Válido");
                    pixCorreto = true;
                }
                else if (pix.Length == 14 && pix.Contains("."))
                {
                    Console.WriteLine("CPF Válido");
                    pixCorreto = true;
                }
                else if (pix.Length == 18 && pix.Contains("/"))
                {
                    Console.WriteLine("CNPJ Válido");
                    pixCorreto = true;
                }
                else if (pix.Length == 11)
                {
                    Console.WriteLine("Número de telefone válido");
                    pixCorreto = true;
                }
                else
                {
                    Console.WriteLine("pix inválido");
                    pix = Console.ReadLine();
                }
            }

            return pix;
        }

        public void MostrarChavePix(string confirmacaoPix)
        {
            Console.WriteLine($"Voce está transferindo para o pix: {confirmacaoPix}");
        }

        public void MostrarValorPix(double confirmacaoValorPix)
        {
            Console.WriteLine($"Voce está transferindo o valor de {confirmacaoValorPix}");
            Console.WriteLine();
        }
    }
}

// ramdom update
