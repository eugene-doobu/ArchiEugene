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
            RefreshScene().Forget();
        }

        private async UniTask RefreshScene()
        {
            gameObject.SetActive(false);
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
            gameObject.SetActive(true);
        }

        public override void Clear()
        {
        
        }
    }
}

