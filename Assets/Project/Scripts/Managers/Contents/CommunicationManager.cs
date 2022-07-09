using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.Communication
{
    public class CommunicationManager
    {
        private Dictionary<NpcType, NpcInfo> _npcTypeToStringDict;
        
        private readonly Dictionary<NpcType, Dictionary<int, TalkContent>> _npcTalkContents =
            new Dictionary<NpcType, Dictionary<int, TalkContent>>();

        public NpcInfo GetNpcInfo(NpcType type) => _npcTypeToStringDict[type];

        public TalkContent GetTalkContent(NpcType type, int index) => _npcTalkContents[type]?[index];

        public void Init()
        {
            InitNpcTypeToStringDict();
            InitNpcTalkContents();
        }

        private void InitNpcTypeToStringDict()
        {
            _npcTypeToStringDict = Managers.Data.LoadJson<NpcData, NpcType, NpcInfo>("NpcData").MakeDict();
        }

        private void InitNpcTalkContents()
        {
            foreach (NpcType npcType in System.Enum.GetValues(typeof(NpcType)))
            {
                var talkContents = 
                    Managers.Data.LoadJson<TalkContentsData, int, TalkContent>($"TalkContent/{npcType.ToString()}")?.MakeDict();
                if (talkContents == null)
                {
                    Debug.LogWarning($"[Communication] {npcType.ToString()}의 TalkContent를 찾을 수 없습니다.");
                    continue;
                }
                _npcTalkContents[npcType] = talkContents;

                foreach (var kvp in _npcTalkContents[npcType])
                {
                    Debug.LogWarning(kvp.Key + ", " + kvp.Value.talkContent);
                }
            }
        }
    }
}
