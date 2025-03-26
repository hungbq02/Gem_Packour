using System;
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
    [Header("PowerUp")]
    private float defaultSpeed;
    private float defaultJumpHeight;
    private bool isTransformed = false;
    public bool IsTransformed { get => isTransformed; }
    public float DefaultSpeed { get => defaultSpeed; }
    public float DefaultJumpHeight { get => defaultJumpHeight; }
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

        defaultSpeed = movementSM.speed;
        defaultJumpHeight = movementSM.jumpHeight;
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
    // LOSE
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

    // POWER UP
    public void ApplySpeedBoost(float multiplier)
    {
        movementSM.speed *= multiplier;
        animator.SetFloat("SpeedMultiplier", multiplier);
    }

    public void ApplyJumpBoost(float multiplier)
    {
        movementSM.jumpHeight *= multiplier;
    }

    public void TransformPlayer(bool transform, float value)
    {
        if (transform)
        {
            Debug.Log("Transformed");
            if (!isTransformed)
            {
                this.transform.localScale *= value;
                isTransformed = true;
            }
        }
        else
        {
            if (isTransformed)
            {
                this.transform.localScale /= value;
                isTransformed = false;
            }
        }
    }
    public void ResetPowerUps()
    {
        ResetSpeed();
        ResetJump();
        TransformPlayer(false, 1f);
    }
    public void ResetSpeed()
    {
        movementSM.speed = defaultSpeed;
    }

    public void ResetJump()
    {
        movementSM.jumpHeight = defaultJumpHeight;
    }
}
