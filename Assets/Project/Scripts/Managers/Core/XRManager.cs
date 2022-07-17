using UnityEngine;
using UnityEngine.XR;
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

        public static Vector3 GetDevicePosition(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.devicePosition, out var value)
                ? value : Vector3.zero;
        }

        public static Quaternion GetDeviceRotation(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.deviceRotation, out var value)
                ? value : Quaternion.identity;
        }
        
        public static Vector3 GetVelocity(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.deviceVelocity, out var value)
                ? value : Vector3.zero;
        }
        
        public static Vector3 GetAngularVelocity(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out var value)
                ? value : Vector3.zero;
        }
        
        public static Vector3 GetAcceleration(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.deviceAcceleration, out var value)
                ? value : Vector3.zero;
        }
        
        public static Vector3 GetAngularAcceleration(XRController controller)
        {
            return InputDevices
                .GetDeviceAtXRNode(controller.controllerNode)
                .TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out var value)
                ? value : Vector3.zero;
        }
    }
}
