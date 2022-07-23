using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ArchiEugene.XRToolkit
{
    /// <summary>
    /// XR Origin에 부착되어 XR 관련 기능들을 관리해주는 컨트롤러 스크립트
    /// </summary>
    public class XROriginController : MonoBehaviour
    {
        private readonly string _handPresencePath = "Prefabs/XR";
        private readonly string _leftHandPresence = "Left Hand Presence";
        private readonly string _rightHandPresence = "Right Hand Presence";
        
        [Header("Controllers")]
        [SerializeField] private XRController leftHand;
        [SerializeField] private XRController rightHand;
        
        [Header("Teleport")]
        [SerializeField] private XRController leftTeleportRay;
        [SerializeField] private XRController rightTeleportRay;
        [SerializeField] private InputHelpers.Button teleportActivationButton;
        [SerializeField] private float activationThreshold = 0.1f;
        
        [Header("Interactor")]
        [SerializeField] private XRRayInteractor leftInteractorRay;
        [SerializeField] private XRRayInteractor rightInteractorRay;

        [Header("Movement")]
        [SerializeField] private float speed = 1;
        [SerializeField] private XRNode inputSource;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float additionalHeight = 0.2f;
        
        private float _fallingSpeed;
        private XROrigin _origin;
        private Vector2 _inputAxis;
        private CharacterController _character;

        private Transform _tr;
        
        public bool EnableLeftTeleport { get; set; } = true;
        public bool EnableRightTeleport { get; set; } = true;
        
        private void Awake()
        {
            InitControllers();

            _tr = GetComponent<Transform>();
            _character = GetComponent<CharacterController>();
            _origin = GetComponent<XROrigin>();
        }

        private void Update()
        {
            if(leftTeleportRay)
            {
                bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);
                leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
            }

            if (rightTeleportRay)
            {
                bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);
                rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
            }
            
            // TODO: Input Manager로 분리 예정
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
        }

        private void FixedUpdate()
        {
            CapsuleFollowHeadset();

            Quaternion headYaw = Quaternion.Euler(0, _origin.Camera.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw *  new Vector3(_inputAxis.x, 0, _inputAxis.y);

            _character.Move(direction * Time.fixedDeltaTime * speed);

            bool isGrounded = CheckIfGrounded();
            if (isGrounded)
                _fallingSpeed = 0;
            else
                _fallingSpeed += gravity * Time.fixedDeltaTime;

            _character.Move(Vector3.up * _fallingSpeed * Time.fixedDeltaTime);
        }

        private void InitControllers()
        {
            if (leftHand == null)
                Debug.LogError("[XRToolkit] LeftHand를 찾을 수 없습니다.");
            else
                leftHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_leftHandPresence}");

            if (rightHand == null)
                Debug.LogError("[XRToolkit] RightHand를 찾을 수 없습니다.");
            else
                rightHand.modelPrefab = Managers.Resource.Load<Transform>($"{_handPresencePath}/{_rightHandPresence}");
        }

        private bool CheckIfActivated(XRController controller)
        {
            controller.inputDevice.IsPressed(teleportActivationButton, out bool isActivated, activationThreshold);
            return isActivated;
        }


        void CapsuleFollowHeadset()
        {
            _character.height = _origin.CameraInOriginSpaceHeight + additionalHeight;
            Vector3 capsuleCenter = transform.InverseTransformPoint(_origin.Camera.transform.position);
            _character.center = new Vector3(capsuleCenter.x, _character.height/2 + _character.skinWidth , capsuleCenter.z);
        }
        
        private bool CheckIfGrounded()
        {
            Vector3 rayStart = _tr.TransformPoint(_character.center);
            float rayLength = _character.center.y + 0.01f;
            bool hasHit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
            return hasHit;
        }
    }
}
