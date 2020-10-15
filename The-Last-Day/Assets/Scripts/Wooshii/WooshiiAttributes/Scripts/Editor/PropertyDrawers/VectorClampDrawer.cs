using System;
using UnityEditor;
using UnityEngine;

namespace WooshiiAttributes
    {
    [CustomPropertyDrawer (typeof (Vector2ClampAttribute))]
    public class Vector2ClampDrawer : WooshiiPropertyDrawer
        {
        private Vector2ClampAttribute target;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
            target = attribute as Vector2ClampAttribute;

            EditorGUI.BeginChangeCheck ();

            EditorGUI.PropertyField (position, property, label, true);

            if (EditorGUI.EndChangeCheck())
                {
                target.value.x = Mathf.Clamp (property.vector2Value.x, target.min, target.max);
                target.value.y = Mathf.Clamp (property.vector2Value.y, target.min, target.max);

                property.vector2Value = target.value;
                }
            }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
            return EditorGUI.GetPropertyHeight (property);
            }
        }

    [CustomPropertyDrawer (typeof (Vector3ClampAttribute))]
    public class Vector3ClampDrawer : WooshiiPropertyDrawer
        {
        private Vector3ClampAttribute target;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
            target = attribute as Vector3ClampAttribute;

            EditorGUI.BeginChangeCheck ();

            EditorGUI.PropertyField (position, property, label, true);

            if (EditorGUI.EndChangeCheck ())
                {
                target.value.x = Mathf.Clamp (property.vector2Value.x, target.min, target.max);
                target.value.y = Mathf.Clamp (property.vector2Value.y, target.min, target.max);
                target.value.z = Mathf.Clamp (property.vector3Value.z, target.min, target.max);

                property.vector2Value = target.value;
                }
            }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
            return base.GetPropertyHeight (property, label);
            }
        }
    }
