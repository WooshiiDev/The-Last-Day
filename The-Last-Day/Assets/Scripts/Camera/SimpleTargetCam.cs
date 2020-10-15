using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using DebugViewer;

namespace LD
    {
    public class SimpleTargetCam : MonoBehaviour
        {
        public enum TargetMode { SINGLE, MULTIPLE }

        public TargetMode targetMode;

        [Header ("Movement Settings")]
        [SerializeField] private bool _isLooking;
        [SerializeField] private bool _isFollowing;

        public float moveSpeed = 8f;
        public float followSpeed = 10f;
        public float rotationSpeed = 30f;

        [Debug ("Camera", "Pitch")] private float pitch;
        [Debug ("Camera", "Yaw")] private float yaw;

        //Componetns
        [SerializeField] private InputHandler inputHandler;

        // --- Privates --- 
        //Delta position
        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private Vector3 inputPosition;

        [SerializeField] private Transform cameraTransform;

        //Targets
        private Transform target;
        private List<Transform> targets;

        private Vector3 finalPosition;

        // --- Properties ---         
        private Vector3 CameraPosition => cameraTransform.position;
        private Quaternion CameraRotation => cameraTransform.rotation;

        public bool IsFollowing => _isFollowing ? TargetsExist () : false;
        public bool IsLooking => _isLooking ? TargetsExist () : false;

        private void OnEnable()
            {
            cameraTransform = Camera.main.transform;
            finalPosition = cameraTransform.position;

            if (inputHandler == null)
                {
                inputHandler = FindObjectOfType<InputHandler> ();
                Debug.LogWarning ("Input Handler was not set, looking through entire hierarchy for component!");
                }
            }

        // Update is called once per frame
        private void Update()
            {
            Quaternion finalQuaternion = Quaternion.identity;
            finalPosition = GetTargetModePosition();

            //Calculate delta for both rotation and position
            float moveTime = Time.deltaTime * (IsFollowing ? followSpeed : moveSpeed);
            float rotationTime = Time.deltaTime * rotationSpeed;

            //Calculate dir for camera to look towards or use input based
            if (IsLooking)
                {
                Vector3 dir = (finalPosition - transform.position).normalized;
                finalQuaternion = Quaternion.LookRotation (cameraTransform.forward, dir);
                }
            else
                {
                finalQuaternion = Quaternion.Euler (pitch, yaw, 0);
                }

            //Set target positions
            targetPosition = Vector3.Lerp (cameraTransform.position, finalPosition, moveTime);
            targetRotation = Quaternion.Slerp (cameraTransform.rotation, finalQuaternion, rotationTime);
            }

        private void LateUpdate()
            {
            //Update position
            cameraTransform.rotation = targetRotation;
            cameraTransform.position = targetPosition;

            inputPosition = targetRotation * inputHandler.MoveVector.ToVector3Plane() ;

            pitch -= inputHandler.PointerVector.y;
            yaw += inputHandler.PointerVector.x;

            yaw = Mathf.Repeat (yaw, 359f);
            pitch = Mathf.Clamp (pitch, -89f, 89f);
            }

        #region Targeting

        public void SetTarget(Transform target)
            {
            this.target = target;
            }

        public void SetTargets(params Transform[] newTargets)
            {
            targets.Clear ();

            for (int i = 0; i < newTargets.Length; i++)
                targets.Add (newTargets[i]);
            }

        #endregion

        #region Helpers

        /// <summary>
        /// Are there existing targets?
        /// </summary>
        /// <returns>Returns true/false depending on if targets are found</returns>
        private bool TargetsExist()
            {
            if (targetMode == TargetMode.SINGLE)
                return target != null;
            else
                return targets.Count > 0;
            }

        /// <summary>
        /// Get the correct target position based on the target mode given
        /// </summary>
        /// <returns> Return the target position required for the target mode. Will return the current position if target modes are invalid</returns>
        private Vector3 GetTargetModePosition()
            {
            //Set position to input by default
            Vector3 position = cameraTransform.position + inputPosition;

            if (IsFollowing)
                {
                position = cameraTransform.position;

                //Set final vector to targets if they exist
                if (TargetsExist ())
                    {
                    if (targetMode == TargetMode.SINGLE)
                        position = target.position;
                    else
                        position = CalculateAveragePosition ();
                    }
                }

            return position;
            }

        /// <summary>
        /// Calculate the average position for multiple targets
        /// </summary>
        /// <returns>Returns the average position if possible. Returns <see cref="Vector3.zero"/> if not target list</returns>
        private Vector3 CalculateAveragePosition()
            {
            if (!TargetsExist())
                {
                Debug.LogError ("Cannot find targets!");
                return transform.position;
                }

            int size = targets.Count;
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < size; i++)
                {
                Transform target = targets[i];
                pos += target.position;
                }

            pos /= size;

            return pos;
            }

        #endregion
        }
    }