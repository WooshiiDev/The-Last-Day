using System;
using UnityEditor;
using UnityEngine;

namespace WooshiiAttributes
    {
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SelectableArray : PropertyAttribute
        {

        }

    //[CustomPropertyDrawer(typeof(SelectableArray))]
    //public class SelectableArrayDrawer : PropertyDrawer
    //    {
    //    private int selected;
    //    private string[] names;
    //    public SerializedProperty arrayProperty;

    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //        {
    //        //Get array path
    //        string path = property.propertyPath;
    //        path = path.Substring (0, path.LastIndexOf ('.'));

    //        //Update the property to the array
    //        arrayProperty = property.serializedObject.FindProperty (path);

    //        if (!arrayProperty.isArray)
    //            {
    //            Debug.LogWarning ("Cannot use use attribute of type SelectableArray as property " + property.displayName + " is not an array.");
    //            base.OnGUI (position, property, label);
    //            return;
    //            }

    //        if (names == null || names.Length != property.arraySize)
    //            names = new string[arrayProperty.arraySize];

    //        for (int i = 0; i < names.Length; i++)
    //            {
    //            names[i] = arrayProperty.GetArrayElementAtIndex (i).displayName;
    //            }

    //        selected = GUILayout.SelectionGrid (selected, names, 4);
    //        property.isExpanded = EditorGUILayout.Foldout (property.isExpanded, property.displayName, true);

    //        if (!property.isExpanded)
    //            return;

    //        EditorGUILayout.PropertyField (arrayProperty.GetArrayElementAtIndex (selected));
    //        }
    //    }
    }
