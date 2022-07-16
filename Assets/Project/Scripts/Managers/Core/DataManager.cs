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

        public TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoader<TKey, TValue>
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            if (ReferenceEquals(textAsset, null)) return default;
            return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
        }

        public TLoader LoadPersistentJson<TLoader, TKey, TValue>(string path) where TLoader : ILoader<TKey, TValue>
        {
            string fullPath = $"{Application.persistentDataPath}/Data/{path}.json";

            bool hasData = File.Exists(fullPath);
            if (!hasData) return default;
            
            string text = File.ReadAllText(fullPath);
            if (text == string.Empty) return default;
            return JsonConvert.DeserializeObject<TLoader>(text);
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


