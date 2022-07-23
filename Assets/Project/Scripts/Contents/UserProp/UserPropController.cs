using System;
using System.Collections;
using System.Collections.Generic;
using ArchiConnect.UI;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ArchiEugene.UserProp
{
    public class UserPropController : MonoBehaviour
    {
        [SerializeField] private InputHelpers.Button userPropWindowButton;
        private GameObject _userPropCanvas;
        
        [Header("Controllers")]
        [SerializeField] private XRController rightHand;
        [SerializeField] private float activationThreshold = 0.1f;

        private GameObject _propObject;
        private float _inputDelay = 0.4f;
        private bool _isDelayed = false;

        private void Start()
        {
            _userPropCanvas = Managers.Resource.Instantiate("UI/XR/Canvas_UserProp");
            _userPropCanvas.GetComponentInChildren<Canvas>().
                gameObject.GetOrAddComponent<UI_UserProp>().Controller = this;
            _userPropCanvas.SetActive(false);
        }

        private void Update()
        { 
            rightHand.inputDevice.IsPressed(userPropWindowButton, out bool rightButtonClick, activationThreshold);

            if (rightButtonClick && !_isDelayed && ReferenceEquals(_propObject, null))
            {
                _userPropCanvas.SetActive(!_userPropCanvas.activeSelf);
                Delay().Forget();
            }

            if (ReferenceEquals(_propObject, null)) return;
            var tr = rightHand.transform;
            var ray = new Ray(tr.position, tr.forward);
            if (!Physics.Raycast(ray, out var hit, float.MaxValue, 1 << Define.LAYER_GROUND)) return;
            _propObject.transform.position = hit.point;
            if (rightButtonClick)
            {
                _propObject = null;
                Managers.UserProp.SaveUserPropData();
            }
        }

        private async UniTask Delay()
        {
            _isDelayed = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_inputDelay));
            _isDelayed = false;
        }

        public void AddUserProp(GameObject obj)
        {
            _propObject = obj;
        }
    }
}
