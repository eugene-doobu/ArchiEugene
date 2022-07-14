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
        private readonly string PROP_JSON_NAME = "PropTransform";
        
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
            _propTransforms = Managers.Data.LoadPersistentJson<PropTransformData, int, PropTransform>(PROP_JSON_NAME).userProps;
        }

        private void InitUserProps()
        {
            foreach (var propData in _propTransforms)
                InstantiateUserProp(propData, false);
        }

        private void SaveUserPropData()
        {
            Managers.Data.SavePersistentJson(_propTransforms, PROP_JSON_NAME);
        }

        public GameObject InstantiateUserProp(int index, Vector3 position, Quaternion rotation)
        {
            var propTransform = new PropTransform(
                index,
                position,
                rotation
            );
            return InstantiateUserProp(propTransform);
        }

        private GameObject InstantiateUserProp(PropTransform propData, bool addList = true)
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
            if(addList) _propTransforms.Add(propData);

            return prop;
        }
    }
}

