//using Director.V2;

//using System.Linq;

//using UnityEditor;
//using UnityEngine;

//namespace Director.Editors
//    {
//    [CustomPropertyDrawer(typeof(DirectorState))]
//    public class CameraDirectorStateEditor : PropertyDrawer
//        {
//        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
//            {
//            GUIStyle window = new GUIStyle (GUI.skin.window)
//                {
//                padding = new RectOffset (2, 0, 0, 0),
//                };

//            //EditorGUILayout.BeginVertical (window);
//            //    {
//                EditorGUILayout.BeginHorizontal ();
//                    {
//                    float defaultWidth = EditorGUIUtility.labelWidth;
//                    EditorGUIUtility.labelWidth = window.CalcSize(new GUIContent(prop.displayName)).x;

//                    prop.isExpanded = EditorGUILayout.Foldout (prop.isExpanded, prop.displayName, true);

//                    //EditorGUI.BeginChangeCheck ();
//                    //    int tempVal = EditorGUILayout.Popup (0, DirectorStatePresets.GlobalStates.Keys.ToArray());
//                    //if (EditorGUI.EndChangeCheck())
//                    //    {
//                    //    var state = DirectorStatePresets.GlobalStates.Keys.ToArray ()[tempVal];

//                    //    var sp = prop.serializedObject.GetIterator ().Copy();

//                    //    while (sp.NextVisible(true))
//                    //        {
//                    //        sp.
//                    //        }
//                    //    }

//                    EditorGUIUtility.labelWidth = defaultWidth;
//                    }
//                EditorGUILayout.EndHorizontal ();

//                //Draw
//                if (prop.isExpanded)
//                    {
//                    SerializedProperty itr = prop.Copy ();
//                    SerializedProperty current = itr.Copy ();

//                    bool iterateChildrenTemp = true;
//                    while (itr.NextVisible (iterateChildrenTemp))
//                        {
//                        iterateChildrenTemp = false;

//                        if (itr.hasVisibleChildren)
//                            iterateChildrenTemp = itr.isExpanded;

//                        //Return if end
//                        if (SerializedProperty.EqualContents (itr, prop.GetEndProperty ()))
//                            break;

//                        EditorGUILayout.PropertyField (itr);
//                        }
//                    }
//            //    }
//            //EditorGUILayout.EndVertical ();

//            //base.OnGUI (position, property, label);
//            }

//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//            {
//            return 0;
//            }
//        }
//    }
