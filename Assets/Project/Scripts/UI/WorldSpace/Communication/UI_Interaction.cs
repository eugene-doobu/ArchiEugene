using System;
using System.Collections;
using System.Collections.Generic;
using ArchiEugene.Communication;
using UnityEngine;

namespace ArchiEugene.UI
{
    public class UI_Interaction : UI_Base
    {
        enum GameObjects
        {
            Group_NpcTalk
        }
        
        public UI_NpcTalk UINpcTalk { get; private set; }

        private NpcType _npcType;
        
        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));

            UINpcTalk = GetObject((int) GameObjects.Group_NpcTalk).GetComponent<UI_NpcTalk>();
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }

        public void StartTalkContent(string content)
        {
            if (ReferenceEquals(UINpcTalk, null)) return;
            UINpcTalk.SetTalkText(content);
        }

        public void SetNpc(NpcType npcType)
        {
            _npcType = npcType;
        }
    }
}