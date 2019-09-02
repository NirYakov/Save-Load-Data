using System;
using System.Collections.Generic;
using System.Text;

namespace SaveLoadData
{
    public interface ISaveLoadData<T> where T : class, new()
    {
        string Path { get; }
        void SaveData(T i_ToSave );
        T LoadData();
    }
}
