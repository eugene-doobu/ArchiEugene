using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.Scene
{
    public class LoginScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            SceneType = Define.Scene.LoginScene;
        }

        public override void Clear()
        {
        }

        [ContextMenu("MoveToWorldScene")]
        public void MoveToWorldScene()
        {
            Managers.Scene.LoadScene(Define.Scene.WorldScene);
        }
    }
}
