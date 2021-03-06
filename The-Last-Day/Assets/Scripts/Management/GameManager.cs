﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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

        public MiniGame currentMiniGame = null;


        public AudioSource Audio { get; private set; }
        public GameObject City { get; private set; }
        public GameObject Player { get; private set; }
        public Countdown WorldTimer { get; private set; }
        public bool GameOver { get; private set; }

        public int Score { get; private set; }

        public string ObjectiveText { get; private set; }

        public Director.CameraFreeLook mainCam;

        private CityGenerator cityGen;

        private void Start()
        {
            GameOver = false;
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
            if (cityGen == null) cityGen = FindObjectOfType<CityGenerator>();
        }

        private void Update()
        {
            if (currentMiniGame == null) Player.GetComponent<LD.ControllerMotor>().canMove = true;
            if (Score < 0) Score = 0;
            // Update timer
            WorldTimer.UpdateTimer(Time.deltaTime);
            City.GetComponent<AudioSource>().volume = GameOver ? 0 : 0.1f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            // Play volcano explosion sound
            if (!Audio.isPlaying && WorldTimer.CurrentTime <= 10)
            {
                Audio.PlayOneShot(eruptionClip);
            }

            // If timer is complete, reset world
            if (WorldTimer.IsFinished)
            {
                GameOver = true;
                //ResetWorld();
            }

            // Activate a deed for a number of random NPCs
            if (CityGenerator.Generated && currentDeeds < maxDeeds)
            {  
                if (cityGen != null)
                {
                    GameObject randomMiniGame = cityGen.data.minigames[Random.Range(0, cityGen.data.minigames.Count)];
                    // Check if its a collectable mini-game
                    if (randomMiniGame == cityGen.data.minigames[0])
                    {
                        randomMiniGame.GetComponent<MiniGame>().objectives[1].objectToCollect = cityGen.data.collectableObjects[Random.Range(0, cityGen.data.collectableObjects.Count)];
                        randomMiniGame.GetComponent<MiniGame>().objectives[1].numToCollect = Random.Range(1, 7);
                    }
                    npcs[Random.Range(0, npcs.Count)].GenerateDeed(randomMiniGame);
                }
            }

            if (currentMiniGame != null)
            {
                ObjectiveText = currentMiniGame.activeObjective.goalText;
            }
            else
            {
                ObjectiveText = "Find a citizen<color=purple> in need </color>to start a <color=green>good deed</color>";
            }
        }

        public void AddScore(int delta)
        {
            Score += delta;
        }
    }
}


