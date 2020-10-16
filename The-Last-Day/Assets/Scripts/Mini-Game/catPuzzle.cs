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
        [SerializeField] private Sprite catFallSprite;
        private Timer timeTillCatFalls;
        public Timer timeTillCatFallsAnyway;
        public bool end;

        // Update is called once per frame
        private void Start()
        {           
            timeTillCatFalls = new Timer(3.4f);
            timeTillCatFallsAnyway = new Timer(7f);
        }
        void Update()
        {
            if (timeTillCatFallsAnyway.IsFinished)
            {
                catFalling = true;
                cat.GetComponent<Image>().sprite = catFallSprite;
                cat.GetComponent<Rigidbody2D>().gravityScale = 10;
            }
            else
            {
                timeTillCatFallsAnyway.UpdateTimer(Time.deltaTime);
            }

            if (numOfClicks >=4)
            {
                catFalling = true;
                cat.GetComponent<Image>().sprite = catFallSprite;
                cat.GetComponent<Rigidbody2D>().gravityScale = 10;
            }

            if (catFalling)
            {
                if (timeTillCatFalls.IsFinished && !end)
                {
                    miniGame.Lose();
                    end = true;
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
            if (catFalling == true && !end)
            {
                miniGame.NextObjective();
                end = true;
                this.gameObject.SetActive(false);             
            }
        }

    } 
}
