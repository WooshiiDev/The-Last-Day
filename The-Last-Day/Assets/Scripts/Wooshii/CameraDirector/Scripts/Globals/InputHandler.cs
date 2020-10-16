using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
    {
    public delegate void OnClick();
    public static OnClick onClick;

    public delegate void OnDrag();
    public static OnDrag onDrag;

    public static Vector3 clickedPosition;
    public static Vector3 draggedPosition;

    public static Vector2 MouseInput { get; private set; }

    public bool spawnCheck;

    public LayerMask layerMask;

    public static InputHandler i;

    private void OnEnable()
        {
        i = this;

        onClick += () => SetVectorFromClick (ref clickedPosition);
        onDrag += () => draggedPosition = GetRaycastLocation ();
        }

    private void OnDisable()
        {
        foreach (Delegate d in onClick.GetInvocationList ())
            onClick -= d as OnClick;

        foreach (Delegate d in onDrag.GetInvocationList ())
            onDrag -= d as OnDrag;
        }

    private void Update()
        {
        float h = Input.GetAxis ("Mouse X");
        float v = Input.GetAxis ("Mouse Y");

        MouseInput.Set (h, v);

        if (Input.GetMouseButtonDown (0))
            onClick?.Invoke ();

        onDrag?.Invoke ();
        }

    public void SetVectorFromClick(ref Vector3 originalVector)
        {
        originalVector = GetRaycastLocation ();
        }

    //Positionals
    private Vector3 GetRaycastLocation()
        {
        //TODO - Add in screen mouse bound checks

        Vector3 retVal = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast (ray, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
            retVal = hit.point;
            }

        return retVal;
        }

    public static bool MouseDown()
        {
        return Input.GetMouseButtonDown (0);
        }

    public static Vector3 MouseScreenPosition => Camera.main.WorldToScreenPoint (draggedPosition);
    }

