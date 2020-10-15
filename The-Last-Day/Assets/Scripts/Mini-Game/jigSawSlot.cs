using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jigSawSlot : MonoBehaviour,IDropHandler
{
    public int slotNum;
    public JigSawPiece assignedPiece;

    public void OnDrop(PointerEventData eventData)
    {

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
            assignedPiece = d.GetComponent<JigSawPiece>();
        }
    }
}
