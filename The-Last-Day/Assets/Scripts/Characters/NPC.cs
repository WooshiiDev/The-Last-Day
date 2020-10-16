using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastDay
{
    public class NPC : MonoBehaviour
    {
        private GameManager game;
        [SerializeField] private GameObject deedMarker = null;
        public bool HasDeed { get; private set; }

        private void Start()
        {
            game = GameManager.instance;
            game.npcs.Add(this);
        }

        private void Update()
        {
            // Display if a deed is available
            deedMarker.SetActive(HasDeed);
        }

        private void LateUpdate()
        {
            // Have UI point towards the camera
            if (game.Player.transform != null) 
                deedMarker.transform.LookAt(game.Player.transform); //TODO Get the camera
        }

        public void ActivateDeed()
        {
            HasDeed = true;
        }

        public void DeactivateDeed()
        {
            HasDeed = false;
            game.currentDeeds--;
        }
    }
}

