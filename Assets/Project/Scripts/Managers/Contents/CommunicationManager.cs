using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.Communication
{
    public class CommunicationManager
    {
        private Dictionary<NpcType, NpcInfo> _npcTypeToStringDict =
            new Dictionary<NpcType, NpcInfo>();

        public NpcInfo GetNpcInfo(NpcType type) => _npcTypeToStringDict[type];

        public void Init()
        {
            InitNpcTypeToStringDict();
        }

        private void InitNpcTypeToStringDict()
        {
            _npcTypeToStringDict = Managers.Data.LoadJson<NpcData, NpcType, NpcInfo>("NpcData").MakeDict();

            foreach (var kvp in _npcTypeToStringDict)
            {
                var info = kvp.Value;
                Debug.Log(kvp.Key + info.email + info.name);                
            }
        }
    }
}
