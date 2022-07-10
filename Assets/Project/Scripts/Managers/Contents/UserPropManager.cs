using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UserProp
{
    public class UserPropManager
    {
        private Dictionary<int, UserProp> _userPropDict = 
            new Dictionary<int, UserProp>();
        
        private Dictionary<int, PropTransform> _propTransform = 
            new Dictionary<int, PropTransform>();

        public UserProp GetUserPropData(int index) => _userPropDict[index];

        public void Init()
        {
            InitUserPropData();
        }

        private void InitUserPropData()
        {
            _userPropDict = Managers.Data.LoadJson<UserPropData, int, UserProp>("UserProps").MakeDict();
        }

        private void InstantiateUserProp(int index, Vector3 position, Quaternion rotation)
        {
            var prop = Managers.Resource.Instantiate($"UserProp/{_userPropDict[index].name}");
            prop.transform.SetPositionAndRotation(position, rotation);
        }
    }
}

