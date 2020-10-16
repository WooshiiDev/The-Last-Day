using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastDay
{
    public class catPuzzle : Puzzle
    {
        private int numOfClicks = 0;
        private bool catFalling;
        [SerializeField] private GameObject cat = null;
        [SerializeField] private Sprite [] catSprites = null;
        public Timer timeTillCatFalls;
        // Update is called once per frame
        private void Start()
        {
            cat.GetComponent<Image>().sprite = catSprites[Random.Range(0, catSprites.Length - 1)];
            timeTillCatFalls = new Timer(3.4f);
        }
        void Update()
        {
            if (numOfClicks >=4)
            {
                catFalling = true;
                cat.GetComponent<Rigidbody2D>().gravityScale = 10;
                //miniGame.NextObjective();
            }
            if (catFalling)
            {
                if (timeTillCatFalls.IsFinished)
                {
                    miniGame.Lose();
                }
                else
                {
                    timeTillCatFalls.UpdateTimer(Time.deltaTime);
                }
            }

        }
        public void addToClicks()
        {
            numOfClicks++;
        }
        public void catchCat()
        {
            if (catFalling == true)
            {
                miniGame.NextObjective();
            }
        }

    } 
}
