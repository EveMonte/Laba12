using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Laba12
{
    class Program
    {
        static void Main(string[] args)
        {
            Reflector.GetAssemblyName();
            Reflector.GetConstructorsInfo();
            Reflector.GetMethodsInfo();
            Reflector.GetInterfacesInfo();
            Reflector.GetFieldsAndPropertiesInfo(typeof(Journey));
            Console.WriteLine(Reflector.toFile(typeof(Journey), @"C:\Users\User\Desktop\ООП\Laba12\Laba12\JourneyInfo.txt", typeof(string)) + "\n");
            Console.WriteLine(Reflector.toFile(typeof(Reflector), @"C:\Users\User\Desktop\ООП\Laba12\Laba12\ReflectorInfo.txt", typeof(bool)) + "\n");
            Console.WriteLine(Reflector.toFile(typeof(Train), @"C:\Users\User\Desktop\ООП\Laba12\Laba12\TrainInfo.txt", typeof(int)) + "\n");

            Console.WriteLine("Enter name of the class");
            string nameOfTheClass = Console.ReadLine();
            Reflector.FindMethodInClass(nameOfTheClass);
            Console.WriteLine(Reflector.Invoke(typeof(Math), "Pow", @"C:\Users\User\Desktop\ООП\Laba12\Laba12\params.txt"));
            List<int> intList = (List<int>)Reflector.Create(typeof(List<int>), new int[] { 10, 12, 13 });
            foreach (int number in intList)
            {
                Console.Write(number + " ");
            }
            
        }
    }
}
