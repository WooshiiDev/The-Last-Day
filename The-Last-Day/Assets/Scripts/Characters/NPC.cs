using UnityEngine;

namespace LastDay
{
    public class NPC : MonoBehaviour
    {
        private GameManager game;
        [SerializeField] private GameObject deedMarker = null;
        public GameObject miniGame;
        public Sprite portraitImage;
        public bool HasDeed { get; private set; }

        private void Start()
        {
            game = GameManager.instance;
            game.npcs.Add(this);
        }

        private void Update()
        {
            // Display if a deed is available
            deedMarker.SetActive(HasDeed);
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

