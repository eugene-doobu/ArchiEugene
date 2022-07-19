using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArchiEugene.UI
{
    public class UI_NpcTalk : UI_Base
    {
        enum Texts
        {
            Text_TalkText
        }

        private TMP_Text _textTalkText;
        private Tween _tween;
        
        public bool IsParentInitSuccess { get; set; }
        
        public override void Init()
        {
            Bind<TMP_Text>(typeof(Texts));
            
            _textTalkText = Get<TMP_Text>((int) Texts.Text_TalkText);
            _textTalkText.text = "";
            ParentInitChecker().Forget();
        }

        private async UniTask ParentInitChecker()
        {
            while (IsParentInitSuccess == false)
                await UniTask.Yield();
            Disable();
        }

        public void Enable()
        {
            _textTalkText.text = "";
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetTalkText(string content, float duration = 1f)
        {
            _textTalkText.text = "";
            _tween?.Kill();
            _tween = _textTalkText.DOText(content, duration);
        }
    }
}