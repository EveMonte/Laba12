using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11lab
{
    partial class Train :IComparable
    {
        public string Destination;
        int TrainNumber;
        public string Time;
        public int Places;
        string value = "     ";
        public string duration = "03:00";
        const string Class = "Business";
        static int Counter = 0;
        public int Size;
        public int hours;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Train otherTrain = obj as Train;
            if (otherTrain != null)
                return this.Places.CompareTo(otherTrain.Places);
            else
                throw new ArgumentException("Object is not a Train");
        }
        /*private Train()
        {
            Console.WriteLine("This is private class, dude! Go on!");
        }*/

        public string Duration
        {
            get
            {
                return duration;
            }
            protected set
            {
                duration = value;
            }
        }
        static readonly string Message = "You can only read me or edit in constructor";
        static Train()
        {
            Console.WriteLine("This is static constructor");
            Console.WriteLine(Message);
        }

        public Train()
        {
            Destination = "Витебск";
            TrainNumber = 703;
            Time = "15:15";
            Places = 50;
            Counter++;
            hours = 15;
        }

        public Train(string Dest, int Numb, string Tim, int places)
        {
            Destination = Dest;
            TrainNumber = Numb;
            Time = Tim;
            Places = places;
            hours = (int)Char.GetNumericValue(Time[0]) * 10 + (int)Char.GetNumericValue(Time[1]);
            Counter++;
        }

        public Train(string Dest, int places, int Numb = 606,  string Tim = "11:40")
        {
            Destination = Dest;
            TrainNumber = Numb;
            Time = Tim;
            Places = places;
            hours = Convert.ToInt32(Tim[0]) * 10 + Convert.ToInt32(Tim[1]);

            Counter++;
        }


        static void CounterOutput()
        {
            Console.WriteLine("Количество созданных экземпляров объекта Train равно {0}", Counter);
        }


    }

    partial class Train
    {
        public void Output()
        {
            Console.WriteLine($"Поезд под номером {this.TrainNumber}" +
                $" отправляется в {this.Destination}" +
                $" в {this.Time}." +
                $" Всего в поезде {this.Places} мест");
        }

    }
}
