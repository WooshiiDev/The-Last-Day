using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastDay
{
    [CreateAssetMenu(fileName = "CityData", menuName = "ScriptableObjects/CityData", order = 1)]

    public class CityData : ScriptableObject
    {
        public List<GameObject> buildings = new List<GameObject>();
    }
}

