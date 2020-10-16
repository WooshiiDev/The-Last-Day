using UnityEngine;

namespace LastDay
{
    public class jigsawPuzzle : Puzzle
    {
        [SerializeField] jigSawSlot[] jigSawSlots = null;
        void Update()
        {
            for (int i = 0; i < jigSawSlots.Length - 1; i++)
            {
                if (jigSawSlots[i].assignedPiece == null) return;
                if (jigSawSlots[i].assignedPiece.pieceNum != jigSawSlots[i].slotNum) return;
                Debug.Log($"{i} SUCCESS");
                if (i == jigSawSlots.Length - 2)
                {
                    miniGame.NextObjective();
                    this.gameObject.SetActive(false);
                }
            }
        }
    } 
}
