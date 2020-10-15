using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastDay
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private GameObject deedMarker = null;
        public bool HasDeed { get; private set; }

        private void Start()
        {
            GameManager.instance.npcs.Add(this);
        }

        private void Update()
        {
            // Display if a deed is available
            deedMarker.SetActive(HasDeed);
        }

        private void LateUpdate()
        {
            // Have UI point towards the camera
            //if (GameManager.instance.Player.transform) deedMarker.transform.LookAt(GameManager.instance.Player.transform); //TODO Get the camera
        }

        public void ActivateDeed()
        {
            HasDeed = true;
        }

        public void DeactivateDeed()
        {
            HasDeed = false;
            GameManager.instance.currentDeeds--;
        }
    }
}

