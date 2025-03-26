using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementSM movementSM;
    private PlayerInputHandler input;
    private CharacterController controller;
    [HideInInspector] public Transform cameraTransform;

    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private float forceMin = 300f;
    [SerializeField] private float forceMax = 600f;

    [Header("Cinemachinee")]
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    public GameObject CinemachineCameraTarget;
    public float CameraAngleOverride = 0.0f;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float sensitivity = 1f;

    [Header("LadderCheck")]
    /*    public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;*/
    public LayerMask ladderLayer;
    public float raycastDistanceCheckLadder = 0.5f;
    public bool inWindZone = false;

    public Vector3 windForce = Vector3.zero;

    [Header("Animation")]
    private Animator animator;
    public float speedDampTime = 0.1f;
    public float velocityDampTime = 0.9f;
    public float rotationDampTime = 0.1f;

    [Header("Gravity")]
    public float gravityMultiplier = 2.5f;
    [HideInInspector] public float gravityValue = -9.81f;

    private const float _threshold = 0.01f;

    [Header("Checkpoint")]
    private Vector3 respawnPosition;



    public PlayerInputHandler Input { get => input; }
    public Animator Animator { get => animator; }
    public CharacterController Controller { get => controller; }

    private void Awake()
    {
        movementSM = GetComponent<MovementSM>();
        input = GetComponent<PlayerInputHandler>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }
    private void Start()
    {
        respawnPosition = transform.position;
        gravityValue *= gravityMultiplier;
        //   transform.transform.position = new Vector3(0, 0.5f, -5.11f);
        // Boss Vector3(-102.400475,-12.3784752,-186.520325)

    }
    private void LateUpdate()
    {
        CameraRotation();
    }
    private void CameraRotation()
    {
        // if there is an inputDir and camera position is not fixed
        if (input.look.sqrMagnitude >= _threshold /*&& !LockCameraPosition*/)
        {
            _cinemachineTargetYaw += input.look.x * sensitivity;
            _cinemachineTargetPitch -= input.look.y * sensitivity;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    //Check if the player is on the stair
    public bool CanClimb()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.05f;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastDistanceCheckLadder, ladderLayer))
        {
            Debug.Log("Ladder detected: " + hit.collider.name);
            return true;
        }
        return false;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        respawnPosition = checkpointPosition;
    }

    public void Die()
    {
        Debug.Log("Player died. Respawning...");
        //  ApplyRandomForce();
        controller.enabled = false;
        animator.enabled = false;
        StartCoroutine(WaitAndRespawn());
    }
    IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }
    private void Respawn()
    {
        transform.position = respawnPosition;
        controller.enabled = true;
        animator.enabled = true;
    }
    public void ApplyRandomForce()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false; // Kích hoạt vật lý
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere; // Tạo hướng ngẫu nhiên
            float randomForce = UnityEngine.Random.Range(forceMin, forceMax); // Tạo lực ngẫu nhiên
            rb.AddForce(randomDirection * randomForce);
        }
    }

    /*    public bool IsGrounded()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }*/

    /*    private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (IsGrounded()) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }*/

}
