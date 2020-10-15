using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jigsawPuzzle : MonoBehaviour
{
    [SerializeField] jigSawSlot[] jigSawSlots;
    [SerializeField] LastDay.MiniGame miniGame;
    void Update()
    {
        for (int i = 0; i < jigSawSlots.Length -1; i++)
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
