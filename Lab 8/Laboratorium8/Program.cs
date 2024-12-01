using System.Reflection;
using Cars.Interfaces;

namespace Laboratorium8
{
    public class Klasa
    {
        private int pole = 0;

        public int GetPole() => pole;

    }

    class Parking : IParking
    {
        public List<ICar> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public List<IProducer> GetAllProducers()
        {
            throw new NotImplementedException();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Klasa klasa = new Klasa();
            Type t1 = klasa.GetType();
            FieldInfo fieldInfo = t1.GetField("pole", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(klasa, 2);
            Console.WriteLine(klasa.GetPole());
            Assembly a = Assembly.LoadFrom(@"C:\Users\kowal\Desktop\Test.dll");
            foreach (var t in a.GetTypes())
            {
                Console.WriteLine(t.FullName);
            }
            
            Type ti = a.GetType("Test.ElementarySchool");
            ConstructorInfo constructorInfo = ti.GetConstructor(new Type[] { typeof(string) });
            Console.WriteLine(constructorInfo);
            var o = Activator.CreateInstance(ti, new object[] {  });
            Console.WriteLine(o.ToString());
            var o2 = constructorInfo.Invoke(new object[] { "Nazwa" });

        }
    }
}