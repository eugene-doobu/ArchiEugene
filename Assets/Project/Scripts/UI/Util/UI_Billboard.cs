using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UI
{
    public class UI_Billboard : MonoBehaviour
    {
        private Transform _tr;

        void Awake()
        {
            _tr = GetComponent<Transform>();
        }

        void LateUpdate()
        {
            _tr.LookAt(Camera.main.transform.position);
        }
    }
}
