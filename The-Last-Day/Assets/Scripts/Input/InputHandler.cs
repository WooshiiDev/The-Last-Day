using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using DebugViewer;

namespace LD
    {
    public class InputHandler : MonoBehaviour
        {
        public Vector2 MoveVector { get; private set; }
        public Vector2 PointerVector { get; private set; }

        [SerializeField] private MainInput input;

        private void Awake()
            {
            //Initialize
            input = input ?? new MainInput ();

            input.Game.Move.Enable ();
            input.Game.Move.performed += HandleMove;
            input.Game.Move.canceled += CancelMove;

            input.Game.Aim.Enable ();
            input.Game.Aim.performed += HandleMouseDelta;
            input.Game.Aim.canceled += HandleMouseDelta;
            }

        private void OnDisable()
            {
            //Make sure to clear all inputs
            input.Game.Move.performed -= HandleMove;
            input.Game.Move.canceled -= CancelMove;
            input.Game.Move.Disable ();

            input.Game.Aim.performed -= HandleMouseDelta;
            input.Game.Aim.canceled -= HandleMouseDelta;

            input.Game.Aim.Disable ();
            }

        private void HandleMove(InputAction.CallbackContext ctx)
            {
            MoveVector = ctx.ReadValue<Vector2> ().normalized;
            }

        private void CancelMove(InputAction.CallbackContext ctx)
            {
            MoveVector = Vector2.zero;
            }

        private void HandleMouseDelta(InputAction.CallbackContext ctx)
            {
            PointerVector = ctx.ReadValue<Vector2> () * 0.5f * 0.1f;
            }


        }
    }
