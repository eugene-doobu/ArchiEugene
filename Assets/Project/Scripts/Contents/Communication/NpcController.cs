using System;
using System.Collections;
using System.Collections.Generic;
using ArchiEugene.UI;
using UnityEngine;

namespace ArchiEugene.Communication
{
    /// <summary>
    /// 접근 시 Interaction 발생, 상호작용 UI 오픈
    /// 상호작용 UI -> 컨텐츠, 교수님 정보보기, 대화하기, 나가기 
    /// </summary>
    public class NpcController : MonoBehaviour
    {
        private readonly string DEFAULT_AUDIO_NAME = "default";
        
        [field: SerializeField] public NpcType NpcType { set; get; }
        
        [SerializeField] private UI_Interaction interactionCanvas;
        [SerializeField] private List<GameObject> _eventObject = new List<GameObject>();

        private Animator _animator;
        private int _currentTalkIndex = 0;
        
        public bool IsOnInteraction { get; private set; } = false;

        private void Start()
        {
            if(interactionCanvas != null) interactionCanvas.SetNpc(NpcType);
            
            interactionCanvas.AddEventTalkTrigger(TalkTrigger);
            
            interactionCanvas.Disable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Define.TAG_PLAYER)) return;
            StartInteraction();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Define.TAG_PLAYER)) return;
            EndInteraction();
        }
        
        public void StartInteraction()
        {
            if (interactionCanvas == null) return;
            _currentTalkIndex = 0;
            IsOnInteraction = true;
            interactionCanvas.Enable();
        }

        public void EndInteraction()
        {
            if (interactionCanvas == null) return;
            IsOnInteraction = false;
            interactionCanvas.Disable();
        }
        
        public void TalkTrigger()
        {
            int talkContentLength = Managers.Communication.GetTalkContentLength(NpcType);
            if (_currentTalkIndex >= talkContentLength)
            {
                EndTalk();
                return;
            }
            if(_currentTalkIndex == 0) StartTalk();
            TalkProcess();
            _currentTalkIndex++;
        }

        private void StartTalk()
        {
            interactionCanvas.SetActiveNpcTalk(true);
        }

        private void TalkProcess()
        {
            interactionCanvas.StartTalkContent(GetTalkContent(_currentTalkIndex));
        }

        private void EndTalk()
        {
            _currentTalkIndex = 0;
            interactionCanvas.SetActiveNpcTalk(false);
        }

        private string GetTalkContent(int index)
        {
            var talkContent = Managers.Communication.GetTalkContent(NpcType, index);
            
            if(talkContent.animation != 0)
                _animator.SetInteger(Define.HASH_NPC_ANIMATION, 0);

            if (!Managers.Sound.Play($"TalkContent/{NpcType}/{index}"))
            {
                Debug.Log($"[Communication] {NpcType}/{index} 오디오 클립을 찾을 수 없습니다");
                Managers.Sound.Play($"TalkContent/{DEFAULT_AUDIO_NAME}");
            }

            return talkContent.talkContent;
        }

#if UNITY_EDITOR
        #region Test
        [Header("Test")]
        [SerializeField] private int talkContentTestIndex = 0;

        [ContextMenu("GetTalkContentTest")]
        public void GetTalkContentTest()
        {
            if(!Application.isPlaying) 
                Debug.Log($"[Communication] 'GetTalkContentTest' 메서드는 플레이중에 실행해주세요");
            Debug.Log(GetTalkContent(talkContentTestIndex));
        }
        #endregion Test
#endif
    }
}
