using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArchiEugene.Scene
{
    public class GameScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            SceneType = Define.Scene.WorldScene;
            Managers.UserProp.InstantiateUserProps().Forget();
        }

        public override void Clear()
        {
        
        }
    }
}

