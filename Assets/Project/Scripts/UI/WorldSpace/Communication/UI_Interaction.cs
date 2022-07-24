using System;
using System.Collections;
using System.Collections.Generic;
using ArchiEugene.Communication;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArchiEugene.UI
{
    public class UI_Interaction : UI_Base
    {
        enum GameObjects
        {
            Group_NpcTalk,
            Group_NpcInfo
        }

        enum Buttons
        {
            Button_TalkTrigger,
            Button_NpcInfo,
            Button_Email
        }
        
        public UI_NpcTalk UINpcTalk { get; private set; }
        public UI_NpcInfo UINpcInfo { get; private set; }

        private NpcType _npcType;
        
        private Button _talkTrigger;
        private Button _npcInfoButton;
        private Button _emailButton;
        
        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<Button>(typeof(Buttons));

            UINpcTalk = GetObject((int) GameObjects.Group_NpcTalk).GetComponent<UI_NpcTalk>();
            UINpcInfo = GetObject((int) GameObjects.Group_NpcInfo).GetComponent<UI_NpcInfo>();
            
            _talkTrigger = GetButton((int) Buttons.Button_TalkTrigger);
            _npcInfoButton = GetButton((int) Buttons.Button_NpcInfo);
            _emailButton = GetButton((int) Buttons.Button_Email);
            
            _npcInfoButton.onClick.AddListener(() => { SetActiveNpcTalk(UINpcInfo.gameObject.activeSelf); });
            UINpcTalk.IsParentInitSuccess = true;
        }

        public void SetActiveNpcTalk(bool value)
        {
            if (value)
            {
                UINpcTalk.Enable();
                UINpcInfo.Disable();
                return;
            }
            UINpcTalk.Disable();
            UINpcInfo.Enable();
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
            if (_talkTrigger == null)
            {
                Debug.LogError("[Communication] Action을 추가할 Button이 존재하지 않습니다.");
                return;
            }
            _talkTrigger.onClick.AddListener(talkTriggerAction);
        }

        public void SetNpc(NpcType npcType)
        {
            _npcType = npcType;
            UINpcInfo.SetNpcInfoText(_npcType).Forget();
        }
    }
}