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
        private readonly string _handPresencePath = "Prefabs/XR";
        private readonly string _leftHandPresence = "Left Hand Presence";
        private readonly string _rightHandPresence = "Right Hand Presence";
        
        [Header("Controllers")]
        [SerializeField] private XRController leftHand;
        [SerializeField] private XRController rightHand;
        
        [Header("LocoMotion")]
        [SerializeField] private XRController leftTeleportRay;
        [SerializeField] private XRController rightTeleportRay;
        [SerializeField] private InputHelpers.Button teleportActivationButton;
        [SerializeField] private float activationThreshold = 0.1f;

        private void Awake()
        {
            InitControllers();
        }

        private void Update()
        {
            // TODO: Null 체크 방식 변경 예정 
            if(leftTeleportRay != null)
            {
                leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
            }

            if (rightTeleportRay != null)
            {
                rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
            }
        }

        private void InitControllers()
        {
            if (leftHand == null || !leftHand.transform.TryGetComponent(out leftHand))
                Debug.LogError("[XRToolkit] LeftHand를 찾을 수 없습니다.");  
            
            if (rightHand == null || !rightHand.transform.TryGetComponent(out rightHand))
                Debug.LogError("[XRToolkit] RightHand를 찾을 수 없습니다.");

            leftHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_leftHandPresence}");
            rightHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_rightHandPresence}");
        }

        private bool CheckIfActivated(XRController controller)
        {
            InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
            return isActivated;
        }
    }
}
