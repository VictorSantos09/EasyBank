namespace EasyBank
{
    public static class IsNullOrEmpty
    {
        public static int IsEmpty(int? input)
        {
            return input.HasValue ? input.Value : 0;
        }
        public static int OutputNotEmpty(int input)
        {
            var checking = true;
            while (checking)
            {
                int? checker = input;
                if (!checker.HasValue)
                {
                    Console.WriteLine("É necessario digitar algo");
                    Console.Write("Digite: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                    checking = false;
                }
            }
            return input;
        }
        public static bool IsNull(string input)
        {
            if (input == null)
            {
                return true;
            }
            return false;
        }
        public static string OutputNotNull(string input)
        {
            var checking = true;
            while (checking)
            {
                var checker = IsNull(input);
                if (checker == true)
                {
                    Console.WriteLine("É necessario digitar algo");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
                }
                else
                {
                    checking = false;
                }
            }
            return input;
        }
    }
}
