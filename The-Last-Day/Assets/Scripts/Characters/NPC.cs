using UnityEngine;

namespace LastDay
{
    public class NPC : MonoBehaviour
    {
        private GameManager game;
        [SerializeField] private GameObject deedMarker = null;
        public GameObject miniGame;
        public Sprite portraitImage;
        private EntityAnimator anim;
        public GameObject inProgress;
        public bool HasDeed { get; private set; }

        private void Start()
        {
            game = GameManager.instance;
            game.npcs.Add(this);
            anim = this.GetComponent<EntityAnimator>();
        }

        private void Update()
        {
            // Display if a deed is available
            deedMarker.SetActive(HasDeed);
            anim.SetAnimationBool("hasDeed", HasDeed);
        }

        private void LateUpdate()
        {
            // Have UI point towards the camera
            if (game.mainCam != null) deedMarker.transform.LookAt(game.mainCam.cam.transform); 
        }

        public void GenerateDeed(GameObject randomMiniGame)
        {
            HasDeed = true;
            Instantiate(randomMiniGame, this.gameObject.transform);
        }

        public void DeactivateDeed()
        {
            HasDeed = false;
            game.currentDeeds--;
        }
    }
}

