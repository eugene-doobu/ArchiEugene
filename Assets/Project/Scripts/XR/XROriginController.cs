using System;
using System.Collections;
using System.Collections.Generic;
using ArchiEugene;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRToolkit
{
    /// <summary>
    /// XR Origin에 부착되어 XR 관련 기능들을 관리해주는 컨트롤러 스크립트
    /// </summary>
    public class XROriginController : MonoBehaviour
    {
        private readonly string _leftHandPath = "Camera Offset/LeftHand Controller";
        private readonly string _rightHandPath = "Camera Offset/RightHand Controller";
        
        private readonly string _handPresencePath = "Prefabs/XR";
        private readonly string _leftHandPresence = "Left Hand Presence";
        private readonly string _rightHandPresence = "Right Hand Presence";
        
        private Transform _tr;
        
        public XRController _leftHand;
        private XRController _rightHand;

        private void Awake()
        {
            _tr = GetComponent<Transform>();
            InitHands();
        }

        private void InitHands()
        {
            var leftHandTr = _tr.Find(_leftHandPath);
            var rightHandTr = _tr.Find(_rightHandPath);
            
            if (ReferenceEquals(leftHandTr, null) || !leftHandTr.TryGetComponent(out _leftHand))
                Debug.LogError("[XRToolkit] LeftHand를 찾을 수 없습니다.");  
            
            if (ReferenceEquals(rightHandTr, null) || !rightHandTr.TryGetComponent(out _rightHand))
                Debug.LogError("[XRToolkit] RightHand를 찾을 수 없습니다.");

            _leftHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_leftHandPresence}");
            _rightHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_rightHandPresence}");
        }
    }
}
