using System;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// talkContent: 대화 내용
    /// animation: 애니메이션 데이타(0: 기존, 1부터 특정 애니메이션 수행)
    /// eventData: 특정 이벤트를 발생, flag 형식
    /// </summary>
    [Serializable]
    public class TalkContent
    {
        public string talkContent;
        public int animation;
        public int eventData;
    }

    [Serializable]
    public class NpcData : ILoader<NpcType, NpcInfo>
    {
        public List<NpcInfo> npcData = new List<NpcInfo>();
        
        public Dictionary<NpcType, NpcInfo> MakeDict()
        {
            return npcData.ToDictionary(info => (NpcType) info.index);
        }
    }
    
    [Serializable]
    public class TalkContentsData : ILoader<int, TalkContent>
    {
        public List<TalkContent> talkContents = new List<TalkContent>();
        
        public Dictionary<int, TalkContent> MakeDict()
        {
            var index = 0; // 0-based index
            var dict = new Dictionary<int, TalkContent>();
            foreach (var talkContent in talkContents)
            {
                dict.Add(index, talkContent);
                index++;
            }
            return dict;
        }
    }
}