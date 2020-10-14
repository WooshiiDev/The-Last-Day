using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

namespace LastDay
{
    public class CityGenerator : MonoBehaviour
    {
        public CityData data;

        public int size;
        public float paddingX, paddingY, offsetDelta, rotationMax;

        private void Start()
        {
            StartCoroutine(GenerateCity());
        }

        public IEnumerator GenerateCity()
        {
            yield return new WaitUntil(() => GenerateBuildings());
            GenerateNPCs();
        }

        // Create a grid
        public bool GenerateBuildings()
        {
            for (int x = -size; x < size; x++)
            {
                for (int y = -size; y < size; y++)
                {
                    int i = Random.Range(0, 2);

                    if (i == 1)
                    {
                        Vector3 offset = Vector3.forward + Vector3.right;
                        offset.x *= Random.Range(-offsetDelta, offsetDelta);
                        offset.z *= Random.Range(-offsetDelta, offsetDelta);

                        //float noise = Mathf.PerlinNoise(x / 100, y / 100);
                        Instantiate(SelectBuilding(), new Vector3(x * paddingX, 0, y * paddingY) + offset, Quaternion.Euler(new Vector3(0, Random.Range(0, rotationMax), 0))).transform.SetParent(transform);
                    }
                }
            }

            return true;
        }

        // Choose a random building from the list
        public GameObject SelectBuilding()
        {
            int index = Random.Range(0, data.buildings.Count);
            return data.buildings[index];
        }

        public void GenerateNPCs()
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(data.npcs[Random.Range(0, data.npcs.Count)], NPCLocation(50), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
        }

        // Find a random location in the area to spawn an NPC
        public Vector3 NPCLocation(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
    }
}

