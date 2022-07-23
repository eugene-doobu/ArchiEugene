using System;
using ArchiEugene;
using ArchiEugene.UserProp;
using UnityEngine;
using UnityEngine.UI;

namespace ArchiConnect.UI
{
    public class UI_UserProp : UI_XRRoot
    {
        enum Buttons
        {
            Button_Well,
            Button_Lamp,
            Button_Chest,
            Button_Commode,
            Button_Chair,
            Button_Box,
            Button_Boiler,
            Button_Bag,
            Button_Anvil,
        }

        private UserPropController _controller;
        public UserPropController Controller
        {
            get => _controller;
            set
            {
                _controller = value;
            }
        }

        private Transform _headTr;
        
        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            foreach (Buttons type in Enum.GetValues(typeof(Buttons)))
            {
                int index = (int) type;
                GetButton(index).onClick.AddListener(() => OnClickAddUserProp(index));
            }
        }

        private void OnEnable()
        {
            var headTr = Camera.main.transform;
            transform.position = headTr.position + headTr.forward * 2 + headTr.up * 0.5f;
            transform.LookAt(headTr);
        }

        private void OnClickAddUserProp(int index)
        {
            var obj = Managers.UserProp.AddUserProp(index, Vector3.zero, Quaternion.identity);
            if(Controller != null) _controller.AddUserProp(obj);
        }
    }
}
