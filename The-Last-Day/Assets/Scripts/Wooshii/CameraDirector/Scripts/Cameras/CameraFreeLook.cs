using UnityEngine;
using WooshiiAttributes;

namespace Director
    {
    [System.Serializable]
    public class InputData
        {
        public float yaw;
        public float pitch;

        public bool isInvertedX;
        public bool isInvertedY;

        public string mouseX = "Mouse X", mouseY = "Mouse Y";

        public Vector2 mouseSens = new Vector2(1, 1);

        public void UpdateInputData()
            {
            float xMul = (isInvertedX) ? -1 : 1;
            float yMul = (isInvertedY) ? -1 : 1;

            pitch += Input.GetAxis (mouseY) * mouseSens.y * yMul; //Local Y-Axis Orbit [Forward-Back Tilt]
            yaw += Input.GetAxis (mouseX) * mouseSens.x * xMul;   //Local X-Axis Orbit [Left-Right Tilt]

            pitch = Mathf.Clamp (pitch, 45f, 90f);
            yaw = Mathf.Repeat (yaw, 360f);
            }
        }

    public class CameraFreeLook : DirectorCamera
        {
        [ReadOnly] public InputData inputData = new InputData();

        [HeaderLine("Collider")]
        public float wallPadding;
        [Range (0f, 1f)] public float radiusDamp;
        public LayerMask collisionMask;

        public float solveDelay;

        //For collision
        private RaycastHit[] hitArray;
        public Ray ray;

        private bool canRotateOverride;

        protected override void Awake()
            {
            base.Awake ();

            radius = currentState.radius;
            transposeRotation = CameraTransform.rotation;

            cam.fieldOfView = settings.fov;

            UpdateMove ();
            UpdateRotation ();
            }

        private void OnValidate()
            {
            //if (cam == null)
            //    Initialize ();

            //cam.fieldOfView = settings.fov;

            //UpdateMove ();
            //UpdateRotation ();
            }

        protected override void Update()
            {
            base.Update ();

            canRotateOverride = Input.GetButton ("Fire2");

            //Update
            InternalUpdate ();

            //Quick zoom addition
            float zoom = Input.GetAxis ("Mouse ScrollWheel");
            currentState.radius -= zoom * 5;
            currentState.radius = Mathf.Clamp (currentState.radius, 5, 25);
            }

        protected override void LateUpdate()
            {
            base.LateUpdate ();

            if (canRotate && canRotateOverride)
                inputData.UpdateInputData ();

            CameraTransform.position = transposePosition;
            CameraTransform.rotation = transposeRotation;
            }

        protected override void FixedUpdate()
            {
            base.FixedUpdate ();
            }

        private void InternalUpdate()
            {
            //Additional right click
            if (currentState.canRotate && canRotateOverride)
                UpdateRotation ();

            if (currentState.canMove)
                UpdateMove ();
            }

        private void UpdateMove()
            {
            if (!canMove || follow == null)
                return;

            //Get postion and local rotation
            Vector3 origin = follow.position;
            Vector3 targetPosition = origin + transposeRotation * new Vector3 (localOffset.x, localOffset.y, localOffset.z - radius);

            //Get hit wall or max distance
            ray = new Ray (origin, (targetPosition - origin).normalized);
            float rayDistance = Mathf.Max (0.5f, SphereCast (ray, wallPadding, (targetPosition - origin).magnitude, collisionMask));

            //Get point on ray
            targetPosition = ray.GetPoint (rayDistance);
            transposePosition = DirectorSmoothing.SmoothDamp (CameraTransform.position, targetPosition, ref transposeVelocity, moveDamp, Time.deltaTime);

            // CameraTransform.position = transposePosition;
            }

        private void UpdateRotation()
            {
            if (!canRotate)
                return;

            Quaternion inputRotation = Quaternion.Euler (inputData.pitch, inputData.yaw, 0);

            if (lookAt != null)
                {
                if (follow != lookAt)
                    transposeRotation = Quaternion.LookRotation ((lookAt.position - CameraTransform.position).normalized, Vector3.up);
                else
                    transposeRotation = Quaternion.Slerp (transposeRotation, inputRotation, Time.deltaTime * rotationDamp);
                }
            else
                {
                transposeRotation = Quaternion.Slerp (transposeRotation, inputRotation, Time.deltaTime * (60 - rotationDamp));
                }

          //  CameraTransform.rotation = transposeRotation;
            }

        #region Targets

        public void SetLookAtTarget(Transform target)
            {
            this.lookAt = target;

            inputData.pitch = CameraTransform.eulerAngles.x;
            inputData.yaw = CameraTransform.eulerAngles.y;
            }

        public void SetFollowTarget(Transform follow)
            {
            this.follow = follow;

            inputData.pitch = CameraTransform.eulerAngles.x;
            inputData.yaw = CameraTransform.eulerAngles.y;
            }

        #endregion

        // ============ Helpers ============
        public Vector3 LocalTransformedPosition(Transform transform, Vector3 vectorModifier = default)
            {
            return transform.position + (transform.rotation * vectorModifier);
            }

        public Vector3 LocalTransformedVector(Transform transform, Vector3 vector)
            {
            return transform.rotation * vector;
            }

        public new void ChangeState(DirectorState newState)
            {
            previousState = currentState;
            currentState = newState;
            }

        public new void ChangeState(string stateName)
            {
            if (DirectorStatePresets.GlobalStates.TryGetValue (stateName, out DirectorState state))
                {
                previousState = currentState;
                currentState = state;
                }
            else
                {
                currentState = previousState;
                }
            }

        private float SphereCast(Ray ray, float radius, float maxDistance, LayerMask collision)
            {
            hitArray = Physics.SphereCastAll (ray, radius, maxDistance, collision, QueryTriggerInteraction.Ignore);
            float min = maxDistance;

            for (int i = 0; i < hitArray.Length; i++)
                {
                float distance = hitArray[i].distance;

                if (distance < min && hitArray[i].collider != null)
                    min = distance;
                }

            return min;
            }
        }
    }
