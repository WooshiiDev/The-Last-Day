using UnityEngine;
using UnityEngine.EventSystems;

namespace LastDay
{
    public class jigSawSlot : MonoBehaviour, IDropHandler
    {
        public int slotNum = 0;
        public JigSawPiece assignedPiece = null;

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
}
