namespace GGJ
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu, optionsMenu, title, credits, tutorial;
        public Image soundImg;
        public Sprite soundIconOn, soundIconOff;

        void Start()
        {
            soundImg.sprite = (AudioListener.volume == 1.0f) ? soundIconOn : soundIconOff;
            InitMenu();
        }

        public void InitMenu()
        {
            title.SetActive(true);
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            credits.SetActive(false);
            tutorial.SetActive(false);
        }

        public void Options()
        {
            title.SetActive(true);
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            credits.SetActive(false);
            tutorial.SetActive(false);
        }

        public void Credits()
        {
            title.SetActive(false);
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            credits.SetActive(true);
            tutorial.SetActive(false);
        }

        public void SoundToggle()
        {
            AudioListener.volume = (AudioListener.volume == 0.0f) ? 1.0f : 0.0f;
            soundImg.sprite = (AudioListener.volume == 1.0f) ? soundIconOn : soundIconOff;
        }

        public void ReadyPlay()
        {
            title.SetActive(false);
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            credits.SetActive(false);
            tutorial.SetActive(true);
        }

        public void Play()
        {
            Game.LoadScene(SCENE.INGAME);
        }

        public void Stop()
        {
            Application.Quit();
        }
    }
}
