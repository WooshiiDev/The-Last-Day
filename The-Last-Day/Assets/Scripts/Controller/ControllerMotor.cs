﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

/// <summary>
/// Created by Damian Slocombe | Wooshii
/// </summary>
namespace LD
    {
    [RequireComponent(typeof(ControllerInput), typeof(NavMeshAgent))]
    public class ControllerMotor : MonoBehaviour
        {
        //Components
        public ControllerInput Input { get; private set; }
        public NavMeshAgent Agent{ get; private set; }

        private VisualEffect effect;

        private LastDay.EntityAnimator entityAnimator;

        private void OnEnable()
            {
            Input = GetComponent<ControllerInput> ();
            Agent = GetComponent<NavMeshAgent> ();

            effect = GetComponentInChildren<VisualEffect> ();

           entityAnimator = GetComponent<LastDay.EntityAnimator>();

            Input.AddClickListener (MoveToPoint);

            }

        private void Awake()
            {
            Input = GetComponent<ControllerInput> ();
            Agent = GetComponent<NavMeshAgent> ();

            Input.AddClickListener (MoveToPoint);
            }

        public void Update()
            {
            effect.enabled = transform.hasChanged;

            if (transform.hasChanged)
                OnMove ();

            entityAnimator.SetAnimationBool("moving",Agent.remainingDistance >.4f);
            }

        /// <summary>
        /// Move the controller to the clicked position
        /// </summary>
        public void MoveToPoint(InputAction.CallbackContext ctx)
            {
            Vector3 mousePosition = Input.mouse.position.ReadValue ();
            Ray ray = Camera.main.ScreenPointToRay (mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                Agent.SetDestination (hit.point);
            }

        public void OnMove()
            {
            effect.SetVector3 ("velocity", transform.InverseTransformDirection(-Agent.velocity));
            }
        }

    }
