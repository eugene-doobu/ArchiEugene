using System;
using System.Collections;
using System.Collections.Generic;
using ArchiEugene.Communication;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArchiEugene.UI
{
    public class UI_Interaction : UI_Base
    {
        enum GameObjects
        {
            Group_NpcTalk
        }

        enum Buttons
        {
            Button_TalkTrigger
        }
        
        public UI_NpcTalk UINpcTalk { get; private set; }

        private NpcType _npcType;
        private Button _nextTalkButton;
        
        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<Button>(typeof(Buttons));

            UINpcTalk = GetObject((int) GameObjects.Group_NpcTalk).GetComponent<UI_NpcTalk>();
            _nextTalkButton = GetButton((int) Buttons.Button_TalkTrigger);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void StartTalkContent(string content)
        {
            if (ReferenceEquals(UINpcTalk, null)) return;
            UINpcTalk.SetTalkText(content);
        }

        public void AddEventTalkTrigger(UnityAction talkTriggerAction)
        {
            if (_nextTalkButton == null)
            {
                Debug.LogError("[Communication] Action을 추가할 Button이 존재하지 않습니다.");
                return;
            }
            _nextTalkButton.onClick.AddListener(talkTriggerAction);
        }

        public void SetNpc(NpcType npcType)
        {
            _npcType = npcType;
        }
    }
}