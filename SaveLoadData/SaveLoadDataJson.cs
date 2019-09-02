using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace SaveLoadData
{
    public class SaveLoadDataJson<T> : ISaveLoadData<T> where T : class , new()
    {
        public const string Ending = "json";

        private string m_Path; 

        public string Path
        {
            get
            {
                return m_Path;
            }

            set
            {
                m_Path = $"{value}.{Ending}";
            }
        }

        public void SaveData(T i_ToSave)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(Path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, i_ToSave);
            }
        }


        public static object Deserialize(string path)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamReader(path))
            using (var reader = new JsonTextReader(sw))
            {
                return serializer.Deserialize(reader);
            }
        }

        public T LoadData()
        {
           // checkOfNullOrWhiteSpaces();
            T item;

            if (File.Exists(Path))
            {
                var serializer = new JsonSerializer();

                using (var sw = new StreamReader(Path))
                using (var reader = new JsonTextReader(sw))
                {
                    item = serializer.Deserialize<T>(reader);
                }
            }
            else
            {
                item = new T();
            }

            return item;
        }

        private void checkOfNullOrWhiteSpaces()
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                throw new ArgumentException($"{nameof(Path)} is null or white spaces.");
            }
        }
    }
}

// item = (T) xs.Deserialize(rd) as T

