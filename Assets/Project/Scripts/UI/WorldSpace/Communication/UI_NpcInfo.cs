using ArchiEugene.Communication;
using DG.Tweening;
using TMPro;
using Cysharp.Text;

namespace ArchiEugene.UI
{
    public class UI_NpcInfo : UI_Base
    {
        enum Texts
        {
            Text_NpcInfo
        }

        private TMP_Text _npcInfoText;
        private Tween _tween;
        
        public override void Init()
        {
            Bind<TMP_Text>(typeof(Texts));
            _npcInfoText = Get<TMP_Text>((int) Texts.Text_NpcInfo);
            Enable();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetNpcInfoText(NpcType npcType)
        {
            var npcInfo = Managers.Communication.GetNpcInfo(npcType);
            
            var sb = ZString.CreateStringBuilder();
            sb.AppendLine("교수님을 소개합니다!");
            sb.AppendLine($"성함: {npcInfo.name}");
            sb.AppendLine($"이메일: {npcInfo.email}");
            sb.AppendLine("담당과목");
            foreach (var subject in npcInfo.subject)
            {
                sb.AppendLine($"{subject}");
            }

            _npcInfoText.text = sb.ToString();
        }
    }
}
