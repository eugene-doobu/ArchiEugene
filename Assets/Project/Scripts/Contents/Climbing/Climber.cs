using ArchiEugene.XRToolkit;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ArchiEugene.Climbing
{
    public class Climber : MonoBehaviour
    {
        public static XRController ClimbingHand;
        
        private CharacterController _character;
        private XROriginController _continuousMovement;

        void Start()
        {
            _character = GetComponent<CharacterController>();
            _continuousMovement = GetComponent<XROriginController>();
        }

        void FixedUpdate()
        {
            if(!ReferenceEquals(ClimbingHand, null))
            {
                _continuousMovement.enabled = false;
                Climb();
            }
            else
            {
                _continuousMovement.enabled = true;
            }
        }

        void Climb()
        {
            InputDevices.GetDeviceAtXRNode(ClimbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
            _character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
        }
    }
}

