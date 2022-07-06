using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.Communication
{
    [Serializable]
    public class NpcInfo
    {
        public int index;
        public string name;
        public string email;
        public string[] subject;
    }

    [Serializable]
    public class NpcData : ILoader<NpcType, NpcInfo>
    {
        public List<NpcInfo> npcData = new List<NpcInfo>();
        
        public Dictionary<NpcType, NpcInfo> MakeDict()
        {
            Dictionary<NpcType, NpcInfo> dict = new Dictionary<NpcType, NpcInfo>();
            foreach (NpcInfo info in npcData)
                dict.Add((NpcType)info.index, info);
            return dict;
        }
    }
}