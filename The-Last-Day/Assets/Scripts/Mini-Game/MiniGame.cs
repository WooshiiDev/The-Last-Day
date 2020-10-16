using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace LastDay
{
    public enum objectiveType { Puzzle, Waypoint, Collect, TalkToNPC, ReturnToNPC }
    public class MiniGame : MonoBehaviour
    {
        public List<Objective> objectives = null;
        [SerializeField] private float timeLimit = 0f;
        private int currentObjective = 0;
        public Objective activeObjective;
        [SerializeField] private Transform player = null;
        public NPC npc;
        public int numCollected = 0;

        private void Start()
        {          
            PrimeObjectives();
        }
        private void Update()
        {
            if (player == null)
            {
                if (GameManager.instance.Player!=null) player = GameManager.instance.Player.transform;
            }
            if (npc == null) npc = this.GetComponentInParent<NPC>();
            else
            {
                if (GameManager.instance.currentMiniGame == this)
                {
                    activeObjective = objectives[currentObjective];
                    UpdateTimeLimit();
                    CheckObjectiveGoal(objectives[currentObjective]);
                }
            }
        }
        public void PrimeObjectives()
        {
            npc = this.GetComponentInParent<NPC>();
            if (GameManager.instance.currentDeeds < GameManager.instance.maxDeeds)
            {
                // Add deed to game
                GameManager.instance.currentDeeds++;
            }
            else
            {
                Destroy(this.gameObject);
            }
            if (timeLimit > 0) { } // Set Time Limit
            // Open Overlay
            if (objectives.Count <= 0) return;
            for (int i = 0; i < objectives.Count; i++)
            {
                objectives[i].objectiveComplete.AddListener(() =>
                {
                    NextObjective();
                });
                objectives[i].isCurrentObjective = false;
            }
            objectives[0].isCurrentObjective = true;           
            SpawnNextObjective(objectives[currentObjective]);
        }
        void UpdateTimeLimit()
        { 
            // LastDay.Utilities.Timer
        }

        public void NextObjective()
        {
            Debug.Log($"Objective Complete : {objectives[currentObjective].goal}");
            if (currentObjective >= objectives.Count - 1)
            {
                Win();
                return;
            }
            currentObjective++;
            for (int i = 0; i <= objectives.Count - 1; i++)
            {
                objectives[i].isCurrentObjective = false;
            }
            SpawnNextObjective(objectives[currentObjective]);
            objectives[currentObjective].isCurrentObjective = true;
            if (player != null) player.GetComponent<LD.ControllerMotor>().canMove = true;
            numCollected = 0;
        }

        private void SpawnNextObjective(Objective objective)
        {
            switch (objective.goal)
            {
                case objectiveType.Puzzle:
                    break;
                case objectiveType.Waypoint:
                    objective.goalText = $"Go to {objective.waypointToReach.gameObject.name}";
                    break;
                case objectiveType.Collect:
                    objective.goalText = $"Collect {objective.numToCollect} {objective.objectToCollect.gameObject.name}s";
                    objective.objectsToCollect = new GameObject[objective.numToCollect];
                    for (int i = 0; i < objective.numToCollect; i++)
                    {
                        GameObject collectObject = Instantiate(objective.objectToCollect, CityGenerator.NavMeshLocation(20, transform), Quaternion.identity,this.transform);
                        objective.objectsToCollect[i] = collectObject;
                    }
                    break;
                case objectiveType.TalkToNPC:
                    objective.goalText = $"Talk to {npc.gameObject.name}";
                    break;
                case objectiveType.ReturnToNPC:
                    objective.goalText = $"Return to {npc.gameObject.name}";
                    break;
                default:
                    break;
            }
        }

        private void CheckObjectiveGoal(Objective objective)
        {
            switch (objective.goal)
            {
                case objectiveType.Puzzle:
                    if (player != null) player.GetComponent<LD.ControllerMotor>().canMove = false;
                    objective.puzzleOverlay.gameObject.SetActive(true);
                    objective.puzzleOverlay.miniGame = this;
                    break;
                case objectiveType.Waypoint:
                    if (Vector3.Distance(player.position,objective.waypointToReach.position) < 2f) objective.objectiveComplete.Invoke();
                    break;
                case objectiveType.Collect:
                    
                    if (objective.objectsToCollect.Length > 0)
                    {
                        activeObjective.goalText = $"Collect {numCollected} / {objective.numToCollect} {objective.objectToCollect.gameObject.name}s";
                        for (int i = 0; i < objective.objectsToCollect.Length; i++)
                        {
                            if (objective.objectsToCollect[i].GetComponent<Collectable>().isCollected == false) return;
                            if (i == objective.numToCollect) objective.objectiveComplete.Invoke();
                        }
                    }
                    objective.objectiveComplete.Invoke();
                    break;
                case objectiveType.TalkToNPC:
                    if (Vector3.Distance(player.position, npc.gameObject.transform.position) < 2f) objective.objectiveComplete.Invoke();
                    break;
                case objectiveType.ReturnToNPC:
                    if (Vector3.Distance(player.position, this.transform.position) < 2f) objective.objectiveComplete.Invoke();
                    break;
                default:
                    break;
            }
        }

        private void Win()
        {
            // Close the overlay
            // Trigger Success Dialogue
            // Play Success Sound
            // Add Karma
            GameManager.instance.AddScore(100);
            Debug.Log("Win Mini-Game");
            GameManager.instance.currentMiniGame = null;
            if (player != null) player.GetComponent<LD.ControllerMotor>().canMove = true;
            npc.DeactivateDeed();
            numCollected = 0;
            Destroy(this.gameObject);
        }

        public void Lose()
        {
            // Close the overlay
            // Trigger Failure Dialogue
            // Play Fail Sound
            // Remove Karma
            Debug.Log("Lose Mini-Game");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (GameManager.instance.currentMiniGame == null)
                {
                    GameManager.instance.currentMiniGame = this;
                }
            }
        }
    }
    [Serializable]
    public class Objective
    {
        public objectiveType goal;
        public string goalText;
        [HideInInspector] public bool isCurrentObjective;
        public UnityEvent objectiveComplete;
        public jigsawPuzzle puzzleOverlay;
        public Transform waypointToReach;
        public GameObject objectToCollect;
        public int numToCollect = 0;
        [HideInInspector] public GameObject[] objectsToCollect;
    }
}
