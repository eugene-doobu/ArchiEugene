using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace ArchiConnect.UI
{
    public abstract class UI_XRRoot : UI_Base
    {
        public override void Init()
        {
            if (TryGetComponent(out Canvas canvas))
            {
                canvas.gameObject.GetOrAddComponent<TrackedDeviceGraphicRaycaster>();
                return;
            }
            Debug.LogError("[UI] Canvas not found");
        }
    }
}
