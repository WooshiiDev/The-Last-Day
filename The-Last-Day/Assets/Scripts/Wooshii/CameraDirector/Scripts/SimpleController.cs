 using UnityEditor;
using UnityEngine;
using WooshiiAttributes;

[RequireComponent (typeof (Rigidbody), typeof (CapsuleCollider))]
public class SimpleController : MonoBehaviour
    {
    [System.Serializable]
    public struct RaycastInfo
        {
        public Vector3 position;
        public Quaternion rotation;

        public Vector3 normal;

        public RaycastInfo(Vector3 position, Quaternion rotation, Vector3 normal)
            {
            this.position = position;
            this.rotation = rotation;

            this.normal = normal;
            }
        }

    [System.Serializable]
    public class InputState
        {
        public float horizontal, vertical;
        public Vector2 inputVector;

        //Camera
        public float pitch, yaw;

        public float InputAmount => Mathf.Clamp (Mathf.Abs (horizontal) + Mathf.Abs (vertical), 0, 1);
        }

    [System.Serializable]
    public class ControllerSettings
        {
        [HeaderLine ("Controller State")]
        public bool canMove = true;
        public bool canRotate = true;
        public bool canJump = true;

        public bool useGravity = true;

        [HeaderLine ("Movement Settings")]
        public float moveSpeed;
        public float dashSpeed;
        public float rotationSpeed;

        [Tooltip ("If toggled on, moving will be relative to the ground slope")]
        public bool velocityRelativeToNormal;

        [HeaderLine ("Jump Settings")]
        public float jumpForce;

        [Tooltip ("If toggled on, jumping will apply a force relative to the movement velocity")]
        public bool jumpRelativeToMovement;

        [Tooltip ("If toggled on, jumping will apply a force relative to the ground normal")]
        public bool jumpRelativeToNormal;

        [Header ("Other Settings")]
        public float maxSlope = 40;
        public float slopeSlideSpeed = 2;

        }

    //============================ Movement and Physics ============================
    [HeaderLine ("Movement and Physics")]
    public ControllerSettings moveSettings;

    public bool isGrounded;
    public bool isRunning;
    public Vector3 jumpHeight = new Vector3 (0, 8, 0);
    public bool hasAttemptedJump;

    [Space (6)]

    public LayerMask groundMask;
    public float gravityMultiplier = 1;
    public float groundCheckDistance = 1.1f;

    //============================ Camera Settings ============================
    [HeaderLine ("Camera Settings")]
    public bool handleCamera = true;

    public float cameraDistance = 4;
    public float cameraRotationSpeed = 8.5f;

    [Range (0.0f, 1.0f)]
    [Tooltip ("How long it takes to move to the required position (in seconds)")]
    public float cameraDampTime = 0.2f;
    public Vector3 cameraOffset = Vector3.up * 2;

    [HeaderLine ("Debugging")]
    public bool showGizmos;
    public bool showGroundRaycast;

    //============================ Privates ============================
    [ReadOnly] [SerializeField] private Vector3 targetVelocity;
    [ReadOnly] [SerializeField] private Vector3 jumpVector;
    [ReadOnly] [SerializeField] private Vector3 groundOffset;

    [ReadOnly] [SerializeField] private Quaternion targetRotation;

    private RaycastHit groundHit;

    [ReadOnly] [SerializeField] private float hitSlopeAngle;

    [ReadOnly] [SerializeField] private Vector3 oldGroundPos;
    [ReadOnly] [SerializeField] private Quaternion oldGroundRot;
    [ReadOnly] [SerializeField] private Transform oldGroundTrans;

    private Vector3 cameraMoveVelocity;
    private Vector3 cameraTargetPosition;
    private Quaternion cameraTargetRotation;

    [ContainedClass ()]
    [SerializeField]
    public InputState inputState = new InputState ();
    private Transform cameraTransform;

    //============================ Components ============================ 
    private Rigidbody body;
    private CapsuleCollider coll;

    //============================ Properties ============================ 
    private Vector3 CameraTarget => ControllerTarget + cameraTargetRotation * (Vector3.forward * -cameraDistance);
    private Vector3 ControllerTarget => body.transform.position + cameraOffset;

    #region Init

    private void OnEnable()
        {
        body = GetComponent<Rigidbody> ();
        coll = GetComponent<CapsuleCollider> ();
        }

    private void Awake()
        {
        OnEnable ();

        cameraTransform = Camera.main.transform;

        inputState.pitch = cameraTransform.rotation.eulerAngles.x;
        inputState.yaw = cameraTransform.rotation.eulerAngles.y;
        
        cameraTransform.position = CameraTarget;
        }

    #endregion

    #region Update

    //Input, Rotation
    private void Update()
        {
        UpdateInput ();

        if (moveSettings.canRotate)
            Rotate ();

        if (isGrounded && Input.GetKeyDown (KeyCode.Space))
            Jump ();
        }

    //Position checks
    private void LateUpdate()
        {

        }

    //Movement of Rigidbody, smoothing with camera
    private void FixedUpdate()
        {
        //if (moveSettings.isGroundRaycastRelative)
        //    isGrounded = Physics.Raycast (transform.position, -groundHit.normal, out groundHit, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
        //else
        isGrounded = Physics.Raycast (transform.position, Vector3.down, out groundHit, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);

        isRunning = Input.GetKey (KeyCode.LeftShift);
        hasAttemptedJump &= isGrounded;

        if (isGrounded)
            {
            //currGroundInfo = new RaycastInfo (groundHit.transform.position, groundHit.transform.rotation, groundHit.normal);
            Move ();
            HandleRelativeGround ();

            oldGroundTrans = groundHit.transform;
            oldGroundPos = oldGroundTrans.position;
            oldGroundRot = oldGroundTrans.rotation;
            }
        else
            {
            if (moveSettings.useGravity)
                body.AddRelativeForce (transform.up * Physics.gravity.y, ForceMode.Acceleration);

            oldGroundTrans = null;
            }

        if (inputState.InputAmount == 0)
            targetVelocity *= Time.fixedDeltaTime;

        body.MovePosition (transform.position + (targetVelocity * Time.fixedDeltaTime) + groundOffset);

        if (handleCamera)
            CameraPhysicsStep ();

        //prevGroundInfo = currGroundInfo;


        }

    #endregion

    private void OnDrawGizmosSelected()
        {
        if (showGizmos)
            {
            if (showGroundRaycast)
                Gizmos.DrawRay (transform.position, Vector3.down * groundCheckDistance);
            }
        }

    private void UpdateInput()
        {
        inputState.horizontal = Input.GetAxis ("Horizontal");
        inputState.vertical = Input.GetAxis ("Vertical");

        inputState.inputVector.Set (inputState.horizontal, inputState.vertical);

        inputState.yaw += Input.GetAxis ("Mouse X");
        inputState.pitch += Input.GetAxis ("Mouse Y");

        inputState.pitch = Mathf.Clamp (inputState.pitch, -89.9f, 89.9f);
        inputState.yaw = Mathf.Repeat (inputState.yaw, 360f);
        }

    private void Rotate()
        {
        Vector3 targetDir = cameraTransform.forward * inputState.vertical + cameraTransform.right * inputState.horizontal;

        targetDir.Normalize ();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion tr = Quaternion.LookRotation (targetDir);
        Quaternion smoothedTargetRotation = Quaternion.Slerp (transform.rotation, tr, Time.fixedDeltaTime * inputState.InputAmount * moveSettings.rotationSpeed);

        transform.rotation = smoothedTargetRotation;
        }

    #region Physics

    private void Move()
        {
        hitSlopeAngle = Vector3.Angle (Vector3.up, groundHit.normal);

        if (hitSlopeAngle <= moveSettings.maxSlope)
            {
            //Calculate velocity [direction facing * inputValue * run/walk]
            targetVelocity = transform.forward * (isRunning ? moveSettings.dashSpeed : moveSettings.moveSpeed);

            //Project on planae
            if (moveSettings.velocityRelativeToNormal)
                {
                targetVelocity.y = 0; //Ignore gravity
                targetVelocity = Vector3.ProjectOnPlane (targetVelocity, groundHit.normal);
                }

            targetVelocity *= inputState.InputAmount;
            }
        }

    //TODO: [OBSELETE] Fix jitter with correct body methods
    private void Jump()
        {
        //Could be inside the if statement, but to keep it updated in the inspector, keep it out here for now (testing purposes)
        jumpVector = Vector3.up;

        if (moveSettings.jumpRelativeToNormal)
            jumpVector = groundHit.normal * moveSettings.jumpForce;

        if (moveSettings.jumpRelativeToMovement)
            jumpVector += targetVelocity.normalized * inputState.InputAmount;

        if (moveSettings.canJump)
            {
            body.velocity = new Vector3 (body.velocity.x, 0f, body.velocity.z);
            body.AddRelativeForce (jumpHeight, ForceMode.Impulse);
            }
        }

    /// <summary>
    /// Fixed Update method for Camera.
    /// Used due to having to interpolate with rigidbody 
    /// </summary>
    private void CameraPhysicsStep()
        {
        cameraTargetRotation = Quaternion.Lerp (cameraTransform.rotation, Quaternion.Euler (inputState.pitch, inputState.yaw, 0), Time.deltaTime * cameraRotationSpeed);
        cameraTargetPosition = Vector3.SmoothDamp (cameraTransform.position, CameraTarget, ref cameraMoveVelocity, cameraDampTime);

        cameraTransform.position = cameraTargetPosition;
        cameraTransform.rotation = cameraTargetRotation;
        }

    //Forgot the stuff from before so this'll do for now

    /// <summary>
    /// Project the velocity on to the ground 
    /// </summary>
    private void HandleRelativeGround()
        {
        if (oldGroundTrans == groundHit.transform)
            {
            Vector3 vecLen = groundHit.transform.position - oldGroundPos;
            groundOffset = vecLen;

            //Quaternion rotLen = groundHit.transform.rotation * Quaternion.Inverse(oldGroundRot);
            //groundOffset = rotLen * vecLen;
            }
        else
        if (oldGroundTrans != null)
            {
            groundOffset = Vector3.zero;
            targetVelocity = Vector3.zero;
            }
        }

    #endregion

    #region Utility
    public void SetPosition(Transform transform)
        {
        this.transform.position = transform.position;
        }

    public void SetPosition(Vector3 point)
        {
        this.transform.position = point;
        }
    #endregion
    }
