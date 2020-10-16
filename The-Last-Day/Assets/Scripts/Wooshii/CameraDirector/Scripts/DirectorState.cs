using UnityEngine;

namespace Director
    {
    [System.Serializable]
    public sealed class DirectorState
        {
        public string stateName;

        public enum RotationType
            {
            NONE        = 0,            // 000000
            YAW         = 1,            // 000001
            PITCH       = 2,            // 000010
            PITCH_YAW   = YAW | PITCH,  // 000011
            }

        //Type data
        public RotationType rotation;

        //State
        public bool canMove = true;
        public bool canRotate = true;

        //Positioning
        public Vector3 originOffset = Vector3.up;
        public float radius = 2;

        //Field of View
        public float targetFOV = 60;
        public float fovDamp = 0.5f;

        }
    }
