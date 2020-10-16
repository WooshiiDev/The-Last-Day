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
            if (game.mainCam != null) deedMarker.transform.LookAt(game.mainCam.transform); 
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

