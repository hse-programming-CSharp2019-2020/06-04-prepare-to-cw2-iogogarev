using System;

using System.Runtime.Serialization.Json;

using System.IO;
using EKRLib;
namespace ConsoleApp65
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Collection<Box> boxes = new Collection<Box>();
            int N;
            do
            {
                Console.WriteLine("N:");
            } while (!int.TryParse(Console.ReadLine(), out N) || N < 1 || N > 100000);
            for (int i = 0; i < N; i++) 
            {
                try
                {
                    boxes.Add(new Box(rnd.Next(-3, 10) + rnd.NextDouble(), rnd.Next(-3, 10) + rnd.NextDouble(),
                        rnd.Next(-3, 10) + rnd.NextDouble(), rnd.Next(-3, 10) + rnd.NextDouble()));
                }
                catch (ArgumentException) 
                {
                    i -= 1;
                }
            }
            foreach(Box box in boxes) 
            {
                Console.WriteLine(box);
            }
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Collection<Box>));
            try
            {
                using (FileStream fs = new FileStream(@"boxes.json", FileMode.Create))
                {
                    ser.WriteObject(fs, boxes);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
