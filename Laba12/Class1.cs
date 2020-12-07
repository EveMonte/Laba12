using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Laba12
{
    public interface ITest
    {
        void EmptyMethod(int a);
    }
    public static class Reflector
    {
        static Journey j = new Journey();
        static Type myType = j.GetType();
        public static string GetAssemblyName()
        {
            //a
            Assembly assem = typeof(Journey).Assembly;
            Console.WriteLine(assem.FullName);
            Console.WriteLine("Конструкторы:");
            return assem.FullName;
        }

        static public bool ContainsPublicConstructors(Type classType)
        {
            return classType.GetConstructors().Length == 0 ? false : true;
        }
        public static IEnumerable<string> GetConstructorsInfo()
        {
            //b
            Type myType = j.GetType();
            foreach (ConstructorInfo ctor in myType.GetConstructors())
            {
                Console.Write(myType.Name + " (");
                // получаем параметры конструктора
                ParameterInfo[] parameters = ctor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }
            return myType.GetConstructors().Select(x => x.ToString());
        }
        public static IEnumerable<string> GetMethodsInfo() 
        {
            int index = 0;
            string[] strArray = new string[10];
            //c
            Console.WriteLine("Методы:");
            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual";
                modificator += method.ReturnType.Name;
                modificator += " ";
                modificator += method.Name;
                modificator += " (";

                //Console.Write(modificator);
                //получаем все параметры
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    modificator += parameters[i].ParameterType.Name;
                    modificator += " ";
                    modificator += parameters[i].Name;

                    //Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                    if (i + 1 < parameters.Length) 
                    {
                        modificator += ", ";

                        //Console.Write(", "); 
                    }
                }
                modificator += ")";
                Console.WriteLine(modificator);
                strArray[index] = modificator;
                index++;
            }
            IEnumerable<string> iString = strArray.Select(str => str);
            return iString;
        }

        public static IEnumerable<string> GetFieldsAndPropertiesInfo(Type classType)
        {
            string modificator = "";
            Console.WriteLine("Поля:");
            foreach (FieldInfo field in classType.GetFields())
            {
                modificator += field.FieldType;
                modificator += ' ';
                modificator += field.Name;
                modificator += ' ';
                Console.WriteLine(modificator);
                modificator = "";
            }
            
            Console.WriteLine("Свойства:");
            foreach (PropertyInfo prop in classType.GetProperties())
            {
                modificator += prop.PropertyType;
                modificator += ' ';
                modificator += prop.Name;
                modificator += ' ';
                Console.WriteLine(modificator);
                modificator = "";
            }
            return classType.GetTypeInfo().DeclaredFields.Select(x => x.Name);
        }

        public static void FindMethodInClass(string name)
        {
            bool flag = false;
            Console.WriteLine("Enter type of value");
            string type = Console.ReadLine();
            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual";
                modificator += method.ReturnType.Name;
                modificator += " ";
                modificator += method.Name;
                modificator += " (";

                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {

                    modificator += parameters[i].ParameterType.Name;
                    modificator += " ";
                    modificator += parameters[i].Name;
                    if (type == parameters[i].ParameterType.Name)
                    {
                        flag = true;
                    }
                    //Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                    if (i + 1 < parameters.Length)
                    {
                        modificator += ", ";

                        //Console.Write(", "); 
                    }
                }
                modificator += ")";
                if (flag)
                {
                    Console.WriteLine(modificator);
                }

            }
        }

        static public IEnumerable<string> GetInterfacesInfo()
        {
            Console.WriteLine("Реализованные интерфейсы:");
            foreach (Type i in myType.GetInterfaces())
            {
                Console.WriteLine(i.Name);
            }
            return myType.GetTypeInfo().ImplementedInterfaces.Select(x => x.Name);
        }
        static public object Invoke(Type classType, string methodName, string filename)
        {
            object rez = null;
            object classObj = Create(classType, new object[] { });


            List<string> paramListString = File.ReadAllText(filename).Split('\n').ToList();


            MethodInfo methodInst = classType.GetMethod(methodName);

            var temp = methodInst.GetParameters();
            List<Type> paramListType = methodInst.GetParameters().Select(x => x.ParameterType).ToList();

            if (paramListString.Count != paramListType.Count) return null;

            var paramListObj = new List<object>();
            for (int i = 0; i < paramListType.Count; i++)
            {
                paramListObj.Add(Convert.ChangeType(paramListString[i], paramListType[i]));
            }

            rez = methodInst.Invoke(classObj, paramListObj.ToArray());
            return rez;
        }

        static public object Invoke(Type classType, string methodName)
        {
            object rez = null;
            object classObj = Create(classType, new object[] { });



            MethodInfo methodInst = classType.GetMethod(methodName);

            var temp = methodInst.GetParameters();
            List<Type> paramListType = methodInst.GetParameters().Select(x => x.ParameterType).ToList();

            var paramListObj = new List<object>();
            for (int i = 0; i < paramListType.Count; i++)
            {
                paramListObj.Add(Activator.CreateInstance(paramListType[i]));
            }

            rez = methodInst.Invoke(classObj, paramListObj.ToArray());
            return rez;

        }

        static public string toFile(Type classType, string fileName, Type methodType = null)
        {
            StreamWriter strWr = new StreamWriter(fileName, false);
            string rez = "";
            rez += "Assembly Name: " + GetAssemblyName() + "\n";
            rez += "Contains public constructors: " + (ContainsPublicConstructors(classType) ? "true" : "false") + "\n";
            if (ContainsPublicConstructors(classType))
            {
                rez += "Public constructors:\n";
                foreach (ConstructorInfo ctor in myType.GetConstructors())
                {
                    rez += classType.Name + " (";
                    // получаем параметры конструктора
                    ParameterInfo[] parameters = ctor.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        rez += parameters[i].ParameterType.Name + " " + parameters[i].Name;
                        if (i + 1 < parameters.Length) rez += ", ";
                    }
                    rez+=")\n";
                }
            }

            rez += "Fields and properties:\n";
            foreach (FieldInfo field in classType.GetFields())
            {
                rez += field.FieldType;
                rez += ' ';
                rez += field.Name + "\n";
            }

            foreach (PropertyInfo prop in classType.GetProperties())
            {
                rez += prop.PropertyType;
                rez += ' ';
                rez += prop.Name;
                rez += "\n";
            }

            rez += "Implemented interfaces:\n";
            foreach (Type i in classType.GetInterfaces())
            {
                rez += i.Name + "\n";
            }

            if (methodType != null)
            {
                rez += $"Methods with {methodType} param:\n";
                foreach (string word in GetFieldsAndPropertiesInfo(classType))
                {
                    rez += word + "\n";
                }
            }
            strWr.WriteLine(rez);
            strWr.Close();
            Console.WriteLine(rez);
            return rez;
        }

        static public object Create(Type classType, params object[] paramList)
        {
            object obj = null;

            if (classType.IsAbstract) return null;
            obj = Activator.CreateInstance(classType, paramList);


            return obj;
        }
    }

    public class Journey : ITest
    {
        public string testProperty { get; set; }
        public int testNumber;
        public char testCharacter;
        public Journey(int num)
        {
            testNumber = num;
        }
        public Journey()
        {
            testNumber = 4;
        }
        private Journey(int num, char c)
        {

        }
        public void EmptyMethod(int a)
        {

        }

        public void NewEmptyMethod(int b, char c)
        {

        }

        public int JustAMethod()
        {
            return 1;
        }
    }
}
