using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UserProp
{
    public class UserPropMono : MonoBehaviour
    {
        [field:SerializeField] public int Index { get; set; }
        [field:SerializeField] public string PropName { get; set; }
    }    
}

