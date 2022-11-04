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

        public void VerificarPix()
        {
            bool pixOk = false;
            Console.WriteLine("Digite o pix que gostaria de transferir (E-MAIL, CPF/CNPJ OU TELEFONE)");
            string pix = Console.ReadLine();

            while (pixOk != true)
            {
                if (pix.Contains("@"))
                {
                    Console.WriteLine("E-mail Válido");
                    pixOk = true;
                }
                else if (pix.Length == 14 && pix.Contains("."))
                {
                    Console.WriteLine("CPF Válido");
                    pixOk = true;
                }
                else if (pix.Length == 18 && pix.Contains("/"))
                {
                    Console.WriteLine("CNPJ Válido");
                    pixOk = true;
                }
                else if (pix.Length == 11)
                {
                    Console.WriteLine("Número de telefone válido");
                    pixOk = true;
                }
                else
                {
                    Console.WriteLine("pix inválido");
                    pix = Console.ReadLine();
                }
            }
        }
    }
}
