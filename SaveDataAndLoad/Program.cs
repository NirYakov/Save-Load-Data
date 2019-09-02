using System;
using System.Collections.Generic;
using System.Linq;
using SaveLoadData;

namespace SaveDataAndLoad
{
    public class Program
    {
        static List<Person> sm_Persons;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            SomeData();

            RunSave();

            // see that the original list change but the saving not changed
            sm_Persons[0].LuckyNumber = -13;

            RunLoadAndPrintConsole();

            Console.ReadLine();
        }

      //  static ISaveLoadData<List<Person>> sm_SaveLoad; // for single saving

        public static ISaveLoadData<List<Person>>[] SaveLoadDatas { get; set; } = new ISaveLoadData<List<Person>>[2];

        static void RunSave()
        {

            #region Singles saving not an array of saving

            //sm_SaveLoad = new SaveLoadDataXml<List<Person>>()
            //{
            //    Path = @"Persons"
            //};


            //sm_SaveLoad = new SaveLoadDataJson<List<Person>>()
            //{
            //    Path = @"PersonsJ"
            //};

            //SaveLoadDatas = new ISaveLoadData<List<Person>>()[2];

            #endregion

            SaveLoadDatas = new ISaveLoadData<List<Person>>[2]
            {
                new SaveLoadDataXml<List<Person>>()
                {
                Path = @"Persons"
                }
                ,
                new SaveLoadDataJson<List<Person>>()
                {
                Path = @"PersonsJ"
                }

            };

            foreach (ISaveLoadData<List<Person>> item in SaveLoadDatas)
            {
                item.SaveData(sm_Persons);
            }

            // sm_SaveLoad.SaveData(sm_Persons); // singel saving

        }

        static void RunLoadAndPrintConsole()
        {
            //List<Person> savedPersons = sm_SaveLoad.LoadData();

            //savedPersons.ForEach(n => Console.WriteLine(n));

            List<Person>[] people = new List<Person>[2];

            people[0] = SaveLoadDatas[0].LoadData();
            people[1] = SaveLoadDatas[1].LoadData();


            foreach (List<Person> list in people)
            {
                list.ForEach(n => Console.WriteLine(n));
                Console.WriteLine();
                Enumerable.Range(0, 10).ToList().ForEach(x => Console.Write('-'));
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Done!");
        }

        static void SomeData()
        {

            sm_Persons = new List<Person>(4)
            {
                    new Person()
                {
                    Name = "Jane Doe",
                    Age = 22.6f,
                    Money = 200_000,
                    LuckyNumber = 7
                },
                   new Person()
                {
                    Name = "Jack",
                    Age = 33f,
                    Money = 21,
                    LuckyNumber = 21
                },
                    new Person()
                {
                    Name = "Bob",
                    Age = 18.2f,
                    Money = 666_000,
                    LuckyNumber = 12
                },
                    new Person()
                {
                    Name = "Vin Benzin",
                    Age = 29.9f,
                    Money = 12_600_000,
                    LuckyNumber = 6
                }
            };

            #region Tried to save with polymorphism to hierarchy
            //sm_Persons = new List<Person>(4) {
            //       new SubPerson()
            //    {
            //        Name = "Jack",
            //        Age = 33f,
            //        Money = 21,
            //        LuckyNumber = 21
            //    },
            //        new SubPerson()
            //    {
            //        Name = "Bob",
            //        Age = 18.2f,
            //        Money = 666_000,
            //        LuckyNumber = 12
            //    },
            //        new SubPerson()
            //    {
            //        Name = "Jane Doe",
            //        Age = 22.6f,
            //        Money = 200_000,
            //        LuckyNumber = 7,
            //        Check = false
            //    }
            //};

            #endregion

        }
    }

    [Serializable]
    public class Person
    {
        //[XmlIgnore]
        public string Name { get; set; } = "n/a";

        public float Age { get; set; }

        public decimal Money { get; set; }

        public int LuckyNumber { get; set; }

        public override string ToString()
        {
            return $" - {Name,-14} , {Age,4} , {Money,10} , {LuckyNumber,3}";
        }
    }

    [Serializable]
    public class SubPerson : Person
    {
        public bool Check { get; set; } = true;

        public override string ToString()
        {
            return base.ToString() + Check;
        }
    }
}
