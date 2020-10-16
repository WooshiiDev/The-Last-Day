using UnityEditor;
using UnityEngine;
using Director;
using Director.Component;

namespace Director.Editors
    {
    [CustomEditor(typeof(DirectorCamera))]
    public class CameraFreeLookEditor : Editor
        {
        private DirectorCamera t;

        private void OnEnable()
            {
            t = target as DirectorCamera;
  
            }
          

        public override void OnInspectorGUI()
            {
            base.OnInspectorGUI ();
            }

        private void LogTypes(SerializedProperty prop)
            {
            while (prop.Next (true))
                Debug.Log (prop.name);
            }

        }

    }
