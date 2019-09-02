using System;
using System.IO;
using System.Xml.Serialization;

namespace SaveLoadData
{
    public class SaveLoadDataXml<T> : ISaveLoadData<T> where T : class , new()
    {
        public const string Ending = "xml";

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
            // checkOfNullOrWhiteSpaces();

            // throw new NotImplementedException();
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(Path))
            {
                xs.Serialize(wr, i_ToSave);
            }
        }

        public T LoadData()
        {
           // checkOfNullOrWhiteSpaces();
            T item;

            if (File.Exists(Path))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamReader rd = new StreamReader(Path))
                {
                    item = xs.Deserialize(rd) as T;
                    if (item == null)
                    {
                        item = new T();
                    }
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

