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

        private bool _isInitData;

        public void InitUserPropData(string content)
        {
            _userPropDict = Managers.Data.LoadJson<UserPropData, int, UserProp>("UserProps").MakeDict();
            LoadUserPropTransformData(content);
            _isInitData = true;
        }

        private void LoadUserPropTransformData(string content)
        {
            if (content == string.Empty)
            {
                _propTransforms = new List<PropTransform>();
                return;
            }
            _propTransforms = Managers.Data.LoadString<PropTransformData, int, PropTransform>(content)?.userProps 
                              ?? new List<PropTransform>();
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

        public async UniTask InstantiateUserProps()
        {
            while (!_isInitData)
                await UniTask.Yield();
            
            foreach (var propData in _propTransforms)
                InstantiateUserProp(propData);
        }
        
        public void SaveUserPropData()
        {
            Managers.Azure.SaveUserData(new PropTransformData(_propTransforms), Define.PROP_JSON_NAME);
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

