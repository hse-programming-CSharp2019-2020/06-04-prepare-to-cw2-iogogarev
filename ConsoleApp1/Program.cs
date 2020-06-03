using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EKRLib;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Collection<Box> boxes = null;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Collection<Box>));
            try
            {
                using (FileStream fs = new FileStream(@"../../../ConsoleApp65/bin/Debug/boxes.json", FileMode.Open))
                {
                    boxes = (Collection<Box>)(ser.ReadObject(fs));
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
            foreach (Box box in boxes) 
            {
                Console.WriteLine(box);
            }
            var first = from box in boxes
                        where (box.GetLongestSideSize() > 3)
                        orderby box.GetLongestSideSize() descending
                        select box;
            Console.WriteLine("First linq");
            foreach(var box in first)
                Console.WriteLine(box);
            Console.WriteLine("Second linq");
            var second = from box in boxes
                         group box by box.Weight;
            foreach (var key in second) 
            {
                Console.WriteLine(key.Key);
                foreach (var val in key) 
                {
                    Console.WriteLine($"\t\t\t{val}");
                }
            }
            var third = from box in boxes
                        where (box.Weight == boxes.Max(x => x.Weight))
                        select box;
            Console.WriteLine("Third linq");
            foreach(var th in third)
                Console.WriteLine(th);
            Console.WriteLine(third.LongCount());
        }
    }
}
