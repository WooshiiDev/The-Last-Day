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
        [SerializeField] private List<Objective> objectives = null;
        [SerializeField] private float timeLimit = 0f;
        private bool isGameActive;
        private int currentObjective = 0;
        [SerializeField] private Transform player = null;

        private void Start()
        {          
            PrimeObjectives();
        }
        private void Update()
        {
            if (player == null)
            {
                player = GameManager.instance.Player.transform;
            }
            if (isGameActive)
            {
                UpdateTimeLimit();
                CheckObjectiveGoal(objectives[currentObjective]);
            }
        }
        public void PrimeObjectives()
        {
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
            isGameActive = true;
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
            objectives[currentObjective].isCurrentObjective = true;
        }

        private void CheckObjectiveGoal(Objective objective)
        {
            switch (objective.goal)
            {
                case objectiveType.Puzzle:
                    objective.puzzleOverlay.gameObject.SetActive(true);
                    objective.puzzleOverlay.miniGame = this;
                    break;
                case objectiveType.Waypoint:
                    if (Vector3.Distance(player.position,objective.waypointToReach.position) < 2f) objective.objectiveComplete.Invoke();
                    break;
                case objectiveType.Collect:
                    objective.objectiveComplete.Invoke();
                    break;
                case objectiveType.TalkToNPC:
                    if (Vector3.Distance(player.position, objective.npcToTalkTo.gameObject.transform.position) < 2f) objective.objectiveComplete.Invoke();
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
            //GameManager.instance.
            Debug.Log("Win Mini-Game");
            isGameActive = false;
        }

        public void Lose()
        {
            // Close the overlay
            // Trigger Failure Dialogue
            // Play Fail Sound
            // Remove Karma
            Debug.Log("Lose Mini-Game");
            isGameActive = false;
        }

    }
    [Serializable]
    public class Objective
    {
        public objectiveType goal;
        [HideInInspector] public bool isCurrentObjective;
        public UnityEvent objectiveComplete;
        public jigsawPuzzle puzzleOverlay;
        public Transform waypointToReach;
        public GameObject[] objectsToCollect;
        public NPC npcToTalkTo;
    }
}
