using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serializable
{
    class Program
    {
        static void Main(string[] args)
        {
            Exml04();
        }

        public static void Exml01()
        {
            person person = new person("Kostya", 22);
            Console.WriteLine("Obj CREATED!");
            //Sozdaem obekt binari formata
            BinaryFormatter formatter = new BinaryFormatter();
            //potok,kuda budem zapisyvat\ object
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);
                Console.WriteLine("Obj serializeble");
            }

            person persondes = new person();
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                var result = formatter.Deserialize(fs);
                persondes = (person)result;
                Console.WriteLine("Obj Deserialize");
                Console.WriteLine("Name: "+persondes.name);
                Console.WriteLine("Year: " + persondes.Year);
            }
        }
        public static void Exml02()
        {
            person person = new person("Kostya", 22);
            Console.WriteLine("Obj CREATED!");
            //Sozdaem obekt binari formata
            SoapFormatter formatter = new SoapFormatter();
            //potok,kuda budem zapisyvat\ object
            using (FileStream fs = new FileStream("peoplee.soap", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);
                Console.WriteLine("Obj serializeble");
            }

            person persondes = new person();
            using (FileStream fs = new FileStream("peoplee.soap", FileMode.OpenOrCreate))
            {
                person newPeople = (person)formatter.Deserialize(fs);
                Console.WriteLine("Obj Deserialize");
                Console.WriteLine("Name: " + persondes.name);
                Console.WriteLine("Year: " + persondes.Year);
            }
        }
        public static void Exml03()
        {
            person person = new person("Kostya", 22);
            Console.WriteLine("Obj CREATED!");
            //Sozdaem obekt binari formata
            XmlSerializer formatter = new XmlSerializer(typeof(person));
            //potok,kuda budem zapisyvat\ object
            using (FileStream fs = new FileStream("peop3le.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);
                Console.WriteLine("Obj serializeble");
            }

            person persondes = new person();
            using (FileStream fs = new FileStream("peop3le.xml", FileMode.OpenOrCreate))
            {
                var result = formatter.Deserialize(fs);
                persondes = (person)result;
                Console.WriteLine("Obj Deserialize");
                Console.WriteLine("Name: " + persondes.name);
                Console.WriteLine("Year: " + persondes.Year);
            }
        }
        public static void Exml04()
        {
            using (WebClient wc = new WebClient())
            {
                string url = "https://api.randomuser.me/?results=1";
                string json = wc.DownloadString(url);
                var data = JsonConvert.DeserializeObject<randomuser>(json);
            }
        }
    }
    [Serializable]
    public class person
    {
        public string name { get; set; }
        public int Year { get; set; }
        public person(string name, int year)
        {
            this.name = name;
            this.Year = year;
        }
        public person()
        {

        }
    }

    public class randomuser
    {
        public List<results> results = new List<results>();
    }
    public class results
    {
        public string gender { get; set; }
        public name name { get; set; }
    }
    public class name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }
}
