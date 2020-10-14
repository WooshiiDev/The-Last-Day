using System;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// Created by Damian Slocombe | Wooshii
/// </summary>
namespace LD
    {
    /// <summary>
    /// Component to handle all input for the player/controller
    /// </summary>
    public class ControllerInput : MonoBehaviour
        {
        public Keyboard keyboard;
        public Mouse mouse;

        [Header("Input Actions")]
        public InputAction onClick;

        private void OnDisable() => onClick.Disable ();

        private void OnEnable() => onClick?.Enable ();

        private void Awake()
            {
            keyboard = Keyboard.current;
            mouse = Mouse.current;
            
            onClick.Enable ();
            }

        public void AddClickListener(Action<InputAction.CallbackContext> callback)
            {
            onClick.performed += callback;
            }
        }
    }
