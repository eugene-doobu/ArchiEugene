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

        private void AddUserProp(UserPropMono userProp)
        {
            var propTransform = new PropTransform(
                userProp.Index,
                userProp.transform.position,
                userProp.transform.rotation
            );
            _propTransforms.Add(propTransform);
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

        private void InstantiateUserProp(PropTransform propData)
        {
            var prop = Managers.Resource.Instantiate($"UserProp/{_userPropDict[propData.propIndex].name}");
            if (ReferenceEquals(prop, null))
            {
                Debug.LogError($"[UserProp] 해당 Prop을 찾을 수 없습니다!");
                return;
            }
            
            var position = new Vector3(propData.positionX, propData.positionY, propData.positionZ);
            var rotation = Quaternion.Euler(new Vector3(propData.rotationX, propData.rotationY, propData.rotationZ));
            prop.transform.SetPositionAndRotation(position, rotation);
        }
    }
}

