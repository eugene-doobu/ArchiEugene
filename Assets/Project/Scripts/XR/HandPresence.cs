using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace XRToolkit
{
    /// <summary>
    /// Hand에 사용할 모델과 컨트롤러를 지정해주는 스크립트
    /// </summary>
    public class HandPresence : MonoBehaviour
    {        
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        private static readonly int Grip = Animator.StringToHash("Grip");

        [SerializeField] private bool _showController;
        
        private InputDevice _targetDevice;
        private GameObject _spawnedController;
        private GameObject _spawnedHandModel;
        private Animator _handAnimator;

        public bool ShowController
        {
            get => _showController;
            set
            {
                _showController = value;
                
                if(_spawnedHandModel != null) 
                    _spawnedHandModel.SetActive(!_showController);
                if(_spawnedController != null) 
                    _spawnedController.SetActive(_showController);
            }
        }

        [field: SerializeField] public InputDeviceCharacteristics ControllerCharacteristics { get; set;} = InputDeviceCharacteristics.None;
        [field: SerializeField] public List<GameObject> ControllerPrefabs { get; set; } = null;
        [field:SerializeField] public GameObject HandModelPrefab { get; set;} = null;

        private void Start()
        {
            TryInitialize();
        }

        private void Update()
        {
            if(!_targetDevice.isValid)
                TryInitialize();
            else
                UpdateHandAnimation();
        }

        private void OnValidate()
        {
            ShowControllerInit();
        }

        private void TryInitialize()
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(ControllerCharacteristics, devices);
            
            foreach (var item in devices)
                Debug.Log($"{item.name}: {item.characteristics}");

            if (devices.Count <= 0) return;
            
            _targetDevice = devices[0];
            var prefab = ControllerPrefabs.Find(controller => controller.name == _targetDevice.name);
            if (prefab)
            {
                _spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("[XRToolkit] 올바른 controller model를 찾을 수 없습니다.");  
                _spawnedController = Instantiate(ControllerPrefabs[0], transform);
            }

            _spawnedHandModel = Instantiate(HandModelPrefab, transform);
            _handAnimator = _spawnedHandModel.GetComponent<Animator>();

            ShowControllerInit();
        }

        private void UpdateHandAnimation()
        {
            if (_showController) return;
            
            if(_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
                _handAnimator.SetFloat(Trigger, triggerValue);
            else
                _handAnimator.SetFloat(Trigger, 0);

            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                _handAnimator.SetFloat(Grip, gripValue);
            else
                _handAnimator.SetFloat(Grip, 0);
        }

        private void ShowControllerInit()
        {
            ShowController = _showController;
        }
    }
}
