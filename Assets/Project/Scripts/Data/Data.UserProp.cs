using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UserProp
{
    [Serializable]
    public class UserProp
    {
        public string name;
        public int cost;
    }

    [Serializable]
    public class PropTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    [Serializable]
    public class UserPropData : ILoader<int, UserProp>
    {
        public List<UserProp> userProps = new List<UserProp>();
        
        public Dictionary<int, UserProp> MakeDict()
        {
            var index = 0; // 0-based index
            var dict = new Dictionary<int, UserProp>();
            foreach (var userProp in userProps)
            {
                dict.Add(index, userProp);
                index++;
            }
            return dict;
        }
    }
    
    [Serializable]
    public class PropTransformData : ILoader<int, PropTransform>
    {
        public List<PropTransform> userProps = new List<PropTransform>();
        
        public Dictionary<int, PropTransform> MakeDict()
        {
            var index = 0; // 0-based index
            var dict = new Dictionary<int, PropTransform>();
            foreach (var userProp in userProps)
            {
                dict.Add(index, userProp);
                index++;
            }
            return dict;
        }
    }
}
