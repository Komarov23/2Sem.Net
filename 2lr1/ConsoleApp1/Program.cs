class Program
{
    static string filepath = "D:\\Учёба\\.Net\\2lr1\\Lorem.txt";

    static void Main(string[] args) {
        int choice = -1;

        while (choice != 0)
        {
            Console.WriteLine("Choose: ");
            Console.WriteLine("1 - Read Symbols from file");
            Console.WriteLine("2 - Do Math");
            Console.WriteLine("0 - Exit");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1: ReadLoremFromFile(); break;
                    case 2: DoMath(); break;
                }
            }
        }
    }

    static void ReadLoremFromFile ()
    {
        Console.Write("Enter words count: ");
        int wordsCount = int.Parse(Console.ReadLine());
        string fileContent = File.ReadAllText(filepath);
        string[] words = fileContent.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < wordsCount; i++)
        {
            Console.Write(words[i] + " ");
        }
        Console.WriteLine();
    }

    static void DoMath ()
    {
        Console.Write("Enter present year: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Enter your age: ");
        int age = int.Parse(Console.ReadLine());
        Console.WriteLine("Congrats! You maybe was born at " + (year - age).ToString() + " year!");
    }
}