namespace EasyBank
{
    public class SafetyPassword
    {

        //static string letraUM = "";
        //static string letraDois = "";
        //static string letraT8es = "";

        //static void SelecionarLetrasChaves()
        //{
        //    letraUM = Console.ReadLine();
        //    letraDois = Console.ReadLine();
        //    letraT8es = Console.ReadLine();
        //}


        public void GerarLetras()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string[] charsarr = new string[6];

            var random = new Random();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int posicaoLetra = random.Next(0, 25);
                    string letra = characters[posicaoLetra].ToString();
                    charsarr[i] = charsarr[i] + letra;

                }
            }

            foreach (var item in charsarr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
