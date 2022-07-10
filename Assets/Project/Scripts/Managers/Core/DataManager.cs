using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

namespace ArchiEugene
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager
    {
        public void Init()
        {
            
        }

        public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            if (ReferenceEquals(textAsset, null)) return default;
            return JsonConvert.DeserializeObject<Loader>(textAsset.text);
        }

        public Loader LoadPersistentJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            string fullPath = $"{Application.persistentDataPath}/Data/{path}.json";

            bool hasData = File.Exists(fullPath);
            if (!hasData) return default;
            
            string text = File.ReadAllText(fullPath);
            if (text == string.Empty) return default;
            return JsonConvert.DeserializeObject<Loader>(text);
        }

        public void SavePersistentJson<T>(T data, string path)
        {
            string fullPath = $"{Application.persistentDataPath}/Data/{path}.json";
            bool hasData = File.Exists(fullPath);

            string jsonData = JsonConvert.SerializeObject(data);
            Debug.LogWarning(jsonData);
            File.WriteAllText(fullPath,jsonData);
        }
    }
}


