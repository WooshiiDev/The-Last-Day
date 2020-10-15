using UnityEditor;
using UnityEngine;

namespace WooshiiAttributes
    {
    public class CollectionSelectionDrawer : WooshiiPropertyDrawer
        {

        public SerializedProperty arrayProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
            //Get array path
            string path = property.propertyPath;
            path = path.Substring (0, path.LastIndexOf ('.'));

            //Update the property to the array
            arrayProperty = property.serializedObject.FindProperty (path);

            if (!arrayProperty.isArray)
                {
                base.OnGUI (position, property, label);
                Debug.LogError ("No array to use with this attribute!");

                return;
                }


            }
        }
    }
