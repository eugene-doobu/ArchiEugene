using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ArchiEugene.UserProp
{
    public class UserPropManager
    {
        private Dictionary<int, UserProp> _userPropDict = 
            new Dictionary<int, UserProp>();

        private List<PropTransform> _propTransforms;

        public UserProp GetUserPropData(int index) => _userPropDict[index];

        public void InitUserPropData()
        {
            _userPropDict = Managers.Data.LoadJson<UserPropData, int, UserProp>("UserProps").MakeDict();
            LoadUserPropTransformData();
        }

        private void LoadUserPropTransformData()
        {
            _propTransforms = Managers.Data.LoadPersistentJson<PropTransformData, int, PropTransform>(Define.PROP_JSON_NAME).userProps;
        }

        public GameObject AddUserProp(int index, Vector3 position, Quaternion rotation)
        {
            return AddUserProp(new PropTransform(index, position, rotation));
        }

        public GameObject AddUserProp(PropTransform userPropData)
        {
            _propTransforms.Add(userPropData);
            return InstantiateUserProp(userPropData);
        }

        private void InstantiateUserProps()
        {
            foreach (var propData in _propTransforms)
                InstantiateUserProp(propData);
        }
        
        public void SaveUserPropData()
        {
            Managers.Azure.SaveUserData(_propTransforms, Define.PROP_JSON_NAME);
        }

        private GameObject InstantiateUserProp(PropTransform propData)
        {
            var prop = Managers.Resource.Instantiate($"UserProp/{_userPropDict[propData.propIndex].name}");
            if (ReferenceEquals(prop, null))
            {
                Debug.LogError($"[UserProp] 해당 Prop을 찾을 수 없습니다!");
                return null;
            }
            
            var position = new Vector3(propData.positionX, propData.positionY, propData.positionZ);
            var rotation = Quaternion.Euler(new Vector3(propData.rotationX, propData.rotationY, propData.rotationZ));
            prop.transform.SetPositionAndRotation(position, rotation);
            return prop;
        }
    }
}

