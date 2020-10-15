using System.Collections.Generic;
using WooshiiAttributes;
using UnityEngine;
using Director.Component;

namespace Director
    {
    [System.Serializable]
    public class CameraSettings
        {
        public float fov = 60;

        // Clipping Planes
        public float clipNear = 0.1f;
        public float clipFar = 5000f;

        public Rect viewportRect = new Rect (0, 0, 1, 1);

        public void UpdateCamera(Camera cam)
            {
            cam.fieldOfView = fov;
            cam.nearClipPlane = clipNear;
            cam.farClipPlane = clipFar;
            cam.rect = viewportRect;
            }
        }

    //TODO: Create custom editor for references
    public class DirectorCamera : DirectorCore
        {
        //Drop downs for director states would be nice, future changes
        [HeaderLine ("Settings")]
        public DirectorState currentState = DirectorStatePresets.CinematicDistant;
        protected DirectorState previousState = DirectorStatePresets.ShoulderView;

        [HideInInspector] public Vector3 offset;
        [HideInInspector] public float radius = 7;
        [HideInInspector] public float fovVelocity;

        public CameraSettings settings = new CameraSettings();

        [Range (0, 10)] public float moveDamp;
        [Range (0, 60)] public float rotationDamp;

        [HeaderLine ("Targets")]
        public bool canRotate = true;
        public bool canMove = true;

        public Transform follow;
        public Transform lookAt;

        [HideInInspector] public Vector3 displacement;
        [HideInInspector] public Vector3 transposeVelocity;

        [HeaderLine ("Components")]
        [HideInInspector]
        public DirectorComponent[] components;

        // -- Privates --
        private Vector3 offsetVel;

        // --- Properties ---
        public Camera cam { get; private set; }
        public Transform CameraTransform => cam.transform;

        public Vector3 transposePosition { get; protected set; }
        public Quaternion transposeRotation { get; protected set; }

        public Vector3 localOffset => transform.rotation * currentState.originOffset;
        public Vector3 TargetOffset => transposeRotation * new Vector3 (localOffset.x, localOffset.y, localOffset.z - currentState.radius);

        protected virtual void Awake()
            {
            Initialize ();
            }

        protected virtual void Update()
            {
            UpdateStateData ();
            ComponentUpdate ();

            //Update camera data
            cam.fieldOfView = settings.fov;
            }

        protected virtual void FixedUpdate()
            {
            ComponentFixedUpdate ();
            }

        protected virtual void LateUpdate()
            {

            }

        protected void Initialize()
            {
            if (currentState == null)
                currentState = DirectorStatePresets.ThirdPersonView;

            radius = currentState.radius;
            settings.fov = currentState.targetFOV;
            offset = currentState.originOffset;

            cam = Camera.main;

            if (cam != null)
                cam.fieldOfView = settings.fov;

            InitalizeComponents ();
            }

        #region Setup Components

        protected void InitalizeComponents()
            {
            components = GetComponents<DirectorComponent> ();

            if (components.Length > 0)
                {
                foreach (var component in components)
                    {
                    component.Initialize ();
                    component.directorCam = this;
                    }
                }
            }

        protected void ComponentUpdate()
            {
            if (components != null)
                {
                foreach (var component in components)
                    component.Execute ();
                }
            }

        protected void ComponentFixedUpdate()
            {
            if (components != null)
                {
                foreach (var component in components)
                    component.FixedExecute ();
                }
            }

        #endregion

        private void UpdateStateData()
            {
            radius = currentState.radius;

            settings.fov = DirectorSmoothing.SmoothDamp (settings.fov, currentState.targetFOV, ref fovVelocity, currentState.fovDamp, Time.deltaTime);
            offset = DirectorSmoothing.SmoothDamp (offset, currentState.originOffset, ref offsetVel, Time.fixedDeltaTime, Time.deltaTime);

            if (Mathf.Abs (settings.fov - currentState.targetFOV) <= 0.001f)
                settings.fov = currentState.targetFOV;

            if (Mathf.Abs (radius - currentState.radius) <= 0.001f)
                radius = currentState.radius;
            }

        public void ChangeState(DirectorState newState)
            {
            previousState = currentState;
            currentState = newState;
            }

        public void ChangeState(string stateName)
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
        }
    }