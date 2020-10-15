using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GroupDrawer : MaterialPropertyDrawer
    {
    public override void OnGUI(Rect position, MaterialProperty prop, System.String label, MaterialEditor editor)
        {
        base.OnGUI (position, prop, label, editor);
        }
    }

