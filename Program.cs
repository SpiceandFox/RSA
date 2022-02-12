using System;

namespace RSA
{
    internal class Program
    {
        static KeyGenerator keyGenerator;
        static void Main(string[] args)
        {
            PickNewValues();
            while (true)
            {
                Console.WriteLine("Enter new message: n");
                Console.WriteLine("Generate new keys: k");
                Console.WriteLine("Quit program:      q");
                switch (Console.ReadLine())
                {
                    case "k":
                        PickNewValues();
                        break;
                    case "n":
                        EnterNewMessage();
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
           
        }
        static void PickNewValues()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter p: ");
                    int p = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter q: ");
                    int q = int.Parse(Console.ReadLine());

                    keyGenerator = new KeyGenerator(p, q);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static void EnterNewMessage()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter new message:");
                    string message = keyGenerator.EncryptMessage(Console.ReadLine());
                    Console.WriteLine("Message encrypted: " + message);
                    Console.WriteLine("Message decrypted: " + keyGenerator.DecryptMessage(message));
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
