using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastDay
{
    public class CityGenerator : MonoBehaviour
    {
        public CityData data;

        public float paddingX, paddingY;

        private void Start()
        {
            GenerateBuildings();
        }

        // Create a grid
        public void GenerateBuildings()
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int i = Random.Range(0, 2);

                    if (i == 1)
                    {
                        int offsetDelta = 100;
                        Vector3 offset = Vector3.forward + Vector3.right;
                        offset.x *= Random.Range(-offsetDelta, offsetDelta);
                        offset.z *= Random.Range(-offsetDelta, offsetDelta);

                        //float noise = Mathf.PerlinNoise(x / 100, y / 100);
                        Instantiate(SelectBuilding(), new Vector3(x * paddingX, 0, y * paddingY) + offset, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))).transform.SetParent(transform);
                    }
                }
            }
        }

        // Choose a random building from the list
        public GameObject SelectBuilding()
        {
            int index = Random.Range(0, data.buildings.Count);
            return data.buildings[index];
        }
    }
}

