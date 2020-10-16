using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastDay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public float timeTillGameOver = 300;
        public int maxDeeds = 3, currentDeeds = 0;
        public List<NPC> npcs = new List<NPC>();

        [SerializeField] private GameObject cityPrefab = null;
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private AudioClip eruptionClip = null;

        public AudioSource Audio { get; private set; }
        public GameObject City { get; private set; }
        public GameObject Player { get; private set; }
        public Countdown WorldTimer { get; private set; }

        public Director.CameraFreeLook mainCam;

        private void Start()
        {
            instance = this;
            Audio = GetComponent<AudioSource>();

            // Spawn key elements
            StartCoroutine(Initialise());

            // Start the timer until world reset
            WorldTimer = new Countdown(timeTillGameOver, timeTillGameOver);
        }

        public IEnumerator Initialise()
        {
            // Spawn the city, wait until it's generated
            if (cityPrefab) City = Instantiate(cityPrefab, Vector3.zero, Quaternion.identity);
            City.transform.SetParent(transform);
            yield return new WaitUntil(() => CityGenerator.Generated);

            // Spawn the player
            if (playerPrefab) Player = Instantiate(playerPrefab, CityGenerator.NavMeshLocation(10, transform), Quaternion.identity);

            // Set the camera target as the player
            mainCam.SetFollowTarget(Player.transform);
            mainCam.SetLookAtTarget(Player.transform);
        }

        private void Update()
        {
            // Update timer
            WorldTimer.UpdateTimer(Time.deltaTime);

            // Play volcano explosion sound
            if (!Audio.isPlaying && WorldTimer.CurrentTime <= 10)
            {
                Audio.PlayOneShot(eruptionClip);
            }

            // If timer is complete, reset world
            if (WorldTimer.IsFinished)
            {
                ResetWorld();
            }

            // Activate a deed for a number of random NPCs
            if (CityGenerator.Generated && currentDeeds < maxDeeds)
            {
                npcs[Random.Range(0, npcs.Count)].ActivateDeed();
                currentDeeds++;
            }
        }

        // Reload the scene
        private void ResetWorld()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


