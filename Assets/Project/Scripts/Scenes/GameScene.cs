using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.Scene
{
    public class GameScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            SceneType = Define.Scene.WorldScene;
        }

        public override void Clear()
        {
        
        }
    }
}

