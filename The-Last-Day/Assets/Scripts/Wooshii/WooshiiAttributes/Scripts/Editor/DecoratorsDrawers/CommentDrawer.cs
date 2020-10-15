using System;
using UnityEditor;
using UnityEngine;

namespace WooshiiAttributes
    {
    [CustomPropertyDrawer (typeof (CommentAttribute))]
    public class CommentDrawer : WooshiiDecoratorDrawer
        {
        private CommentAttribute Comment => attribute as CommentAttribute;
        private readonly GUIStyle style = new GUIStyle (EditorStyles.boldLabel);

        public override void OnGUI(Rect rect)
            {
            switch (Comment.messageType)
                {
                case CommentAttribute.MessageType.NONE:
                    EditorGUI.HelpBox (rect, Comment.text, MessageType.None);
                    break;
                case CommentAttribute.MessageType.WARNING:
                    EditorGUI.HelpBox (rect, Comment.text, MessageType.Warning);
                    break;
                case CommentAttribute.MessageType.INFO:
                    EditorGUI.HelpBox (rect, Comment.text, MessageType.Info);
                    break;
                case CommentAttribute.MessageType.ERROR:
                    EditorGUI.HelpBox (rect, Comment.text, MessageType.Error);
                    break;
                }
            }

        //How tall the GUI is for this decorator
        public override float GetHeight()
            {
            return base.GetHeight ();
            }
        }
    }

