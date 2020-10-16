using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

namespace LastDay
{
    public class CityGenerator : MonoBehaviour
    {
        public static bool Generated;
        public CityData data;

        [Header("Buildings")]
        public int size;
        public int density;
        public float paddingX = 6.5f, paddingY = 6.5f, offsetDelta = 0, rotationMax = 360;

        [Header("Trees")]
        public int treeCount = 80;
        public float treeDistance = 100;

        [Header("People")]
        public int characterCount = 30;
        public float characterDistance = 50;

        private void Start()
        {
            // Begin generating the city
            StartCoroutine(GenerateCity());
        }

        // Generate the city
        public IEnumerator GenerateCity()
        {
            yield return new WaitUntil(() => GenerateBuildings());
            GenerateObjects(data.trees, treeCount, treeDistance, true);
            GenerateObjects(data.npcs, characterCount, characterDistance, false);
            yield return new WaitForEndOfFrame();
            Generated = true;
        }

        // Generate a grid of buildings
        public bool GenerateBuildings()
        {
            for (int x = -size; x < size; x++)
            {
                for (int y = -size; y < size; y++)
                {
                    // Chance to spawn a building
                    int i = UnityEngine.Random.Range(0, density);

                    if (i > 0)
                    {
                        // Translate the buildings from their position
                        Vector3 offset = Vector3.forward + Vector3.right;
                        offset.x *= UnityEngine.Random.Range(-offsetDelta, offsetDelta);
                        offset.z *= UnityEngine.Random.Range(-offsetDelta, offsetDelta);

                        // Distance the buildings from each other and add offset
                        Vector3 location = new Vector3(x * paddingX, 0, y * paddingY) + offset;
                        // Randomise rotation
                        Quaternion rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, rotationMax), 0));

                        // Spawn the building
                        GameObject building = Instantiate(SelectRandom(data.buildings), location, rotation);
                        building.transform.SetParent(transform);
                    }
                }
            }

            return true;
        }

        // Choose a random object from a list
        public GameObject SelectRandom(List<GameObject> objects)
        {
            int index = UnityEngine.Random.Range(0, objects.Count);
            return objects[index];
        }

        // Scatter an object type across the area
        public void GenerateObjects(List<GameObject> objects, int count, float distance, bool randomiseScale)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject placeholderObject = SelectRandom(objects);
                GameObject obj = Instantiate(placeholderObject, NavMeshLocation(distance, transform), Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0)));
                if (randomiseScale) obj.transform.localScale *= UnityEngine.Random.Range(0.5f, 1.5f);
                obj.transform.SetParent(transform);
                obj.name = placeholderObject.name;
            }
        }

        // Find a random location in the area using the navmesh
        public static Vector3 NavMeshLocation(float radius, Transform contextTransform)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
            randomDirection += contextTransform.position;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
    }
}

