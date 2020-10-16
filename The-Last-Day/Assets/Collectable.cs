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
            this.GetComponent<MeshRenderer>().enabled = false;
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
