using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SaveLoadData
{
    public class SaveLoadDataLocal<T> : ISaveLoadData<T> where T : class , new()
    {
        private T m_SavedLocal;

        public const string Ending = "txt";

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
            m_SavedLocal = i_ToSave.DeepClone();
        }

        public T LoadData()
        {

            T item = m_SavedLocal;

            if (m_SavedLocal == null)
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

    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T item)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, item);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}

// item = (T) xs.Deserialize(rd) as T

