using UnityEditor;
using UnityEngine;

namespace WooshiiAttributes
    {
    [CustomPropertyDrawer (typeof (ArrayElements))]
    public class ArrayElementsDrawer : WooshiiPropertyDrawer
        {
        // --- Draw Refs ---
        private Texture2D AddTex => EditorGUIUtility.Load ("icons/d_winbtn_graph_max_h.png") as Texture2D;
        private Texture2D RemoveTex => EditorGUIUtility.Load ("icons/d_winbtn_graph_min_h.png") as Texture2D;

        public int index = 0;
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
                Debug.LogError ("Array Elements useless as property is not an array!");

                return;
                }
            else
                {
                index = 0;

                for (int i = 0; i < arrayProperty.arraySize; i++)
                    {
                    if (arrayProperty.GetArrayElementAtIndex (i).displayName == property.displayName)
                        {
                        index = i;
                        break;
                        }
                    }

                EditorGUI.BeginProperty (position, label, property);

                position.width *= 0.9f;

                Rect rectR = position;
                rectR.x += rectR.width;
                rectR.width = 19f;

                EditorGUI.PropertyField (position, property, true);

                DrawButtonLabel (rectR, RemoveTex, "", () =>
                    {
                    arrayProperty.DeleteArrayElementAtIndex (index);
                    });

                rectR.x += 19f;

                DrawButtonLabel (rectR, AddTex, "", () =>
                    {
                    arrayProperty.InsertArrayElementAtIndex (index + 1);
                    });


                property.serializedObject.ApplyModifiedProperties ();
                property.serializedObject.Update ();
                EditorGUI.EndProperty ();
                }
            }

        public override System.Single GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
            if (property.isExpanded)
                return EditorGUI.GetPropertyHeight (property, label, true);
            else
                return base.GetPropertyHeight (property, label);
            }

        private bool DrawButtonLabel(Texture2D texture, string label, System.Action action)
            {

            bool pressed = false;
            GUILayout.BeginVertical ();
                {
                if (pressed = GUILayout.Button (texture, EditorStyles.centeredGreyMiniLabel))
                    action?.Invoke ();
                else
                if (!string.IsNullOrWhiteSpace (label))
                    if (pressed = GUILayout.Button (label, EditorStyles.centeredGreyMiniLabel))
                        action?.Invoke ();
                }
            GUILayout.EndVertical ();

            return pressed;
            }

        private bool DrawButtonLabel(Rect rect, Texture2D texture, string label, System.Action action)
            {

            bool pressed = false;
            GUILayout.BeginVertical ();
                {
                if (pressed = GUI.Button (rect, texture, EditorStyles.centeredGreyMiniLabel))
                    action?.Invoke ();
                else
                if (!string.IsNullOrWhiteSpace (label))
                    {
                    rect.y += 8;
                    if (pressed = GUI.Button (rect, label, EditorStyles.centeredGreyMiniLabel))
                        action?.Invoke ();
                    }

                }
            GUILayout.EndVertical ();

            return pressed;
            }
        }
    }
