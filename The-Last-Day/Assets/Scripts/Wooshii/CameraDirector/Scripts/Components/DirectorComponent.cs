using UnityEngine;

namespace Director.Component
    {
    [System.Serializable]
    public abstract class DirectorComponent : MonoBehaviour
        {
        // -- Components -- 
        public DirectorCamera directorCam;

        public abstract void Initialize();

        public abstract void Execute();

        public abstract void FixedExecute();
        }
    }
