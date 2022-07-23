using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArchiEugene.Emotion
{
    public class EmotionManager
    {
        public async UniTask Smile()
        {
            var obj = Managers.Resource.Instantiate("Emotion/EmojiSmile");
            
            var headTr = Camera.main.transform;
            obj.transform.position = headTr.position + headTr.forward * 2 + headTr.up * 0.5f;
            
            await UniTask.Delay(TimeSpan.FromMilliseconds(1200));
            Managers.Resource.Destroy(obj);
        }
        
        public async UniTask Heart()
        {
            var obj = Managers.Resource.Instantiate("Emotion/EmojiHeart");
            
            var headTr = Camera.main.transform;
            obj.transform.position = headTr.position + headTr.forward * 2 + headTr.up * 0.5f;
            
            await UniTask.Delay(TimeSpan.FromMilliseconds(1200));
            Managers.Resource.Destroy(obj);
        }
    }
}

