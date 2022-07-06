using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ArchiEugene.XRToolkit
{
    public class XRManager
    {
        public void Init()
        {
            GameObject go = GameObject.Find("@InteractionManager");
            if (go == null)
            {
                go = new GameObject { name = "@InteractionManager" };
                go.AddComponent<XRInteractionManager>();
                Object.DontDestroyOnLoad(go);
            }
        }
    }
}
