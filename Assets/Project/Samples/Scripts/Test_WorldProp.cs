using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ArchiEugene.Test
{
    public class Test_WorldProp : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        private GameObject _currentObject;

        private void Update()
        {
            if (_currentObject == null)
            {
                _currentObject = AddWorldProp(Vector3.zero);
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << Define.LAYER_GROUND))
            {
                _currentObject.transform.position = hit.point;
                
                if (Input.GetMouseButtonDown(0))
                {
                    _currentObject = null;
                }
            }
        }

        private GameObject AddWorldProp(Vector3 position)
        {
            var index = int.Parse(_inputField.text) % 9;
            return Managers.UserProp.AddUserProp(index, position ,Quaternion.identity);
        }
    }
}

