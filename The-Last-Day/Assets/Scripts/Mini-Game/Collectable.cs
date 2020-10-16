using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastDay
{
    public class Collectable : MonoBehaviour
    {
        public bool isCollected;

        public void Collect()
        {
            isCollected = true;
            this.GetComponentInParent<MiniGame>().numCollected++;
            this.GetComponent<Collider>().enabled = false;
            this.GetComponentInChildren<MeshRenderer>().enabled = false;
            this.GetComponentInChildren<UnityEngine.VFX.VisualEffect>().enabled =false;
            this.GetComponentInChildren<Light>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }
    } 
}
