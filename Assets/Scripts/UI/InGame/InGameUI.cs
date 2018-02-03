namespace GGJ
{
    using UnityEngine;
    using UnityEngine.UI;

    public class InGameUI : Singleton<InGameUI>
    {
        [SerializeField]
        private Text m_lifeText = null;
        [SerializeField]
        private Text m_foodText = null;
        [SerializeField]
        private Text m_timeText = null;
        
        [SerializeField]
        private Slider m_slider = null;

        [SerializeField]
        private Button m_okButton = null;

        [SerializeField]
        private Button m_optionButton = null;

        [SerializeField]
        private Image m_soundButton = null;

        [SerializeField]
        private GameObject m_optionsPanel = null;
        
        [SerializeField]
        private GameObject m_gameOverPanel = null;

        [SerializeField]
        private GameObject m_endGamePanel = null;

        [SerializeField]
        private Bat m_bat = null;

        [SerializeField]
        private float m_range = 20.0f;

        [SerializeField]
        private Sprite[] m_soundsSprites = null;

        private float m_sonarRange = 0.0f;
        public float sonarRanger
        {
            get
            {
                return m_sonarRange;
            }
        }

        private void Awake()
        {
            Instance = this;
            m_gameOverPanel.SetActive(false);
            m_endGamePanel.SetActive(false);

            m_soundButton.sprite = m_soundsSprites[(AudioListener.volume == 0.0f) ? 0 : 1];

            //(AudioListener.volume == 0.0f);

            //fazer mudar sprite de som se tiver com som desligado
        }

        public void SetLives (int lives)
        {
            m_lifeText.text = lives.ToString();
        }

        public void SetFood(int foods)
        {
            m_foodText.text = foods.ToString();
        }

        public void SetTime(int time)
        {
            m_timeText.text = time.ToString();
        }
        
        public void ActiveSlider()
        {
            m_slider.gameObject.SetActive(true);
            m_slider.value = 3f;
            OnValueChanged(0);
        }
        
        public void OnValueChanged(int valor)
        {
            int sliderValue = Mathf.RoundToInt(m_slider.value);

            m_bat.range = sliderValue;

            m_sonarRange = m_range - sliderValue * 2.0f;
        }

        public void OnClickFinish ()
        {
            m_bat.PlaySonar();
            m_slider.gameObject.SetActive(false);
        }

        public void OnClickOptions()
        {
            m_optionsPanel.SetActive(!m_optionsPanel.activeSelf);

            if (m_optionsPanel.activeSelf)
            {
                Game.Pause();
                m_okButton.interactable = false;
                m_slider.interactable = false;
            }
            else
            {
                Game.Resume();
                m_okButton.interactable = true;
                m_slider.interactable = true;
            }
        }

        public void OnClickSom()
        {
            AudioListener.volume = (AudioListener.volume == 0.0f) ? 1.0f : 0.0f;
            m_soundButton.sprite = m_soundsSprites[(AudioListener.volume == 0.0f) ? 0 : 1];
        }

        public void OnClickExit()
        {
            Game.Resume();
            Game.LoadScene(SCENE.MAIN);
        }

        public void Restart ()
        {
            Game.Resume();
            Game.LoadScene(SCENE.INGAME);
        }

        public void GameOver()
        {
            Game.Pause();
            m_gameOverPanel.SetActive(true);
            m_optionButton.interactable = false;
        }

        public void EndGame()
        {
            m_endGamePanel.SetActive(true);
            GetComponent<Animator>().SetTrigger("EndGame");
        }
        
    }
}
