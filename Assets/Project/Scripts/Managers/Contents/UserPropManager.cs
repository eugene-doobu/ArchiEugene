using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UserProp
{
    public class UserPropManager
    {
        private Dictionary<int, UserProp> _userPropDict = 
            new Dictionary<int, UserProp>();

        private List<PropTransform> _propTransforms;

        public UserProp GetUserPropData(int index) => _userPropDict[index];

        public void Init()
        {
            InitUserPropData();
            LoadUserPropTransformData();
        }

        private void InitUserPropData()
        {
            _userPropDict = Managers.Data.LoadJson<UserPropData, int, UserProp>("UserProps").MakeDict();
        }

        private void LoadUserPropTransformData()
        {
            _propTransforms = Managers.Data.LoadPersistentJson<PropTransformData, int, PropTransform>("PropTransform").userProps;
        }

        private void InstantiateUserProps()
        {
            foreach (var propData in _propTransforms)
                InstantiateUserProp(propData);
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

