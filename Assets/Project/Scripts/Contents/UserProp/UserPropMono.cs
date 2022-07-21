using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UserProp
{
    public class UserPropMono : MonoBehaviour
    {
        [field:SerializeField] public int Index { get; set; }
        [field:SerializeField] public string PropName { get; set; }
        

#if UNITY_EDITOR
        #region Test
        [ContextMenu("SaveUserPropData")]
        public void SaveUserPropData()
        {
            if(!Application.isPlaying) 
                Debug.Log($"[UserProp] 'UserPropSave' 메서드는 플레이중에 실행해주세요");
            Managers.UserProp.SaveUserPropData();
        }
        #endregion Test
#endif
    }    
}

