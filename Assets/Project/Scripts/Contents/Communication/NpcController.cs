using System;
using System.Collections;
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
        [field: SerializeField] public NpcType NpcType;
        [SerializeField] private UI_Interaction interactionCanvas;

        public bool IsOnInteraction { get; private set; } = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Define.PLAYER_TAG)) return;
            StartInteraction();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Define.PLAYER_TAG)) return;
            EndInteraction();
        }
        
        public void StartInteraction()
        {
            if (interactionCanvas == null) return;
            IsOnInteraction = true;
            interactionCanvas.Enable();
        }

        public void EndInteraction()
        {
            if (interactionCanvas == null) return;
            IsOnInteraction = false;
            interactionCanvas.Disable();
        }
        
        public void StartTalk()
        {
            
        }
    }
}
