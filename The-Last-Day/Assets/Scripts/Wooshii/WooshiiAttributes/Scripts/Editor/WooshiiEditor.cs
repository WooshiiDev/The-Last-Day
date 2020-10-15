using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEditor;

namespace WooshiiAttributes
    {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class WooshiiEditor : Editor
        {
        private static IEnumerable<FieldInfo> selectableArray;
        private static List<SerializedProperty> properties = new List<SerializedProperty>();

        private static Dictionary<string, int> arraySelection;

        private int count;

        private Texture2D AddTex => EditorGUIUtility.Load ("icons/d_winbtn_mac_max_h.png") as Texture2D;
        private Texture2D RemoveTex => EditorGUIUtility.Load ("icons/d_winbtn_mac_min_h.png") as Texture2D;

        private void OnEnable()
            {
            selectableArray = GetFields (t => t.GetCustomAttributes(typeof(SelectableArray), true).Length > 0);
            count = selectableArray.Count ();

            arraySelection = new Dictionary<string, int> ();
            properties = new List<SerializedProperty> ();

            }

        public override void OnInspectorGUI()
            {
            if (count == 0)
                {
                DrawDefaultInspector ();
                return;
                }
            serializedObject.Update ();
            GetVisibleProperties (ref properties);

            int i = 0;
            foreach (var property in properties.ToArray())
                {
                bool hasAttr = false;
                foreach (var item in selectableArray)
                    {
                    hasAttr = item.Name == property.name;

                    if (hasAttr)
                        break;
                    }

                if (hasAttr)
                    {
                    if (!arraySelection.ContainsKey (property.name))
                        arraySelection.Add (property.name, 0);

                    DrawSelectableArray (property, property.name);
                    i++;
                    }
                else
                    EditorGUILayout.PropertyField (property);
                }
            }

        private void DrawSelectableArray(SerializedProperty prop, string element)
            {
            if (!prop.isArray)
                {
                EditorGUILayout.PropertyField (prop);
                return;
                }

            string[] names = new string[prop.arraySize];

            if (prop.arraySize == 0)
                {
                DrawButtonLabel (AddTex, "Add Element", () =>
                    {
                    prop.InsertArrayElementAtIndex (0);

                    serializedObject.ApplyModifiedProperties ();
                    serializedObject.Update ();
                    });

                return;
                }
            else
                {
                EditorGUILayout.BeginHorizontal ();
                    {
                    DrawButtonLabel (AddTex, "Add Element", () =>
                        {
                        prop.InsertArrayElementAtIndex (prop.arraySize);

                        serializedObject.ApplyModifiedProperties ();
                        serializedObject.Update ();
                        });

                    DrawButtonLabel (RemoveTex, "Remove Combo", () =>
                        {
                        prop.DeleteArrayElementAtIndex (0);
                        arraySelection[element] = Mathf.Clamp (arraySelection[element]--, 0, prop.arraySize);

                        serializedObject.ApplyModifiedProperties ();
                        serializedObject.Update ();
                        });
                    }
                EditorGUILayout.EndHorizontal ();
                }

            for (int i = 0; i < names.Length; i++)
                names[i] = prop.GetArrayElementAtIndex (i).displayName;

            EditorGUILayout.LabelField ("Selection (" + prop.arraySize + ")");

            int val;
            EditorGUI.BeginChangeCheck ();
                {
                int width = Mathf.Min (4, names.Length);
                val = arraySelection[element] = GUILayout.SelectionGrid (arraySelection[element], names, 3);
                }
            if (EditorGUI.EndChangeCheck())
                {
                serializedObject.ApplyModifiedProperties ();
                serializedObject.Update ();
                }

            EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex(val));
            }

        private void GetVisibleProperties(ref List<SerializedProperty> property)
            {
            properties.Clear ();

            using (var iterator = serializedObject.GetIterator ())
                {
                if (iterator.NextVisible (true))
                    {
                    do
                        {
                        property.Add (serializedObject.FindProperty (iterator.name));
                        }
                    while (iterator.NextVisible (false));
                    }
                }
            }

        private void DrawProperties()
            {
            foreach (var prop in properties)
                {
                EditorGUILayout.PropertyField (prop);

                var a = prop.serializedObject;

                prop.serializedObject.ApplyModifiedProperties ();
                prop.serializedObject.Update ();
                }

            
            }

        private IEnumerable<FieldInfo> GetFields(Func<FieldInfo, bool> condition)
            {

            return target.GetType ().GetFields (BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance).Where(condition);
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
        }
    }