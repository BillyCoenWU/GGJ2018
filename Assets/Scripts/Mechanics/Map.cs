namespace GGJ
{
    using UnityEngine;
    using UnityEngine.U2D;
    using System.Collections;
    using System.Collections.Generic;

    public class Map : Singleton<Map>
    {
        public enum PHASE
        {
            NIGHT,
            DAY
        }

        private int m_currentBehaviour = 0;

        private PHASE m_phase = PHASE.NIGHT;
        public PHASE phase
        {
            get
            {
                return m_phase;
            }
        }
        
        private SpriteAtlas m_mapAtlas = null;

        private List<GGJMonoBehaviour> m_dayBehavours = null;
        private List<GGJMonoBehaviour> m_nightBehavours = null;

        private HexaTile[,] m_map = null;
        public HexaTile GetTile(int x, int y)
        {
            return m_map[x, y];
        }
        
        [SerializeField]
        private int m_foodCount = 0;

        [SerializeField]
        private int m_enemysCount = 0;

        [SerializeField]
        private int m_obstaclesCount = 0;

        [Space(5.0f)]

        [SerializeField]
        private int m_mapSizeX = 23;
        public int mapSizeX
        {
            get
            {
                return m_mapSizeY;
            }
        }

        [SerializeField]
        private int m_mapSizeY = 40;
        public int mapSizeY
        {
            get
            {
                return m_mapSizeX;
            }
        }

        [Space(5.0f)]

        [SerializeField]
        private Bat m_bat = null;

        [SerializeField]
        private SpriteRenderer m_blackPlane = null;

        [Space(5.0f)]
        
        [SerializeField]
        private Vector2 m_mapOffSet = Constantes.VECTOR_TWO_ZERO;
        
        private void Awake()
        {
            Instance = this;

            m_dayBehavours = new List<GGJMonoBehaviour>();
            m_nightBehavours = new List<GGJMonoBehaviour>();

            m_mapAtlas = Resources.Load<SpriteAtlas>(Constantes.ATLAS_PATH);
        }

        private void Start()
        {
            InitMap();
        }

        public void ChangePhase ()
        {
            m_phase = m_phase == PHASE.DAY ? PHASE.NIGHT : PHASE.DAY;

            m_currentBehaviour = 0;

            if (m_phase == PHASE.DAY)
            {
                StartCoroutine(PlaneToDay());
            }
            else
            {
                StartCoroutine(PlaneToNight());
            }
        }

        public void NightAct ()
        {
            if (m_currentBehaviour >= m_nightBehavours.Count)
            {
                ChangePhase();
            }
            
            m_nightBehavours[m_currentBehaviour].InitAction();

            m_currentBehaviour++;
        }

        public void DayAct ()
        {
            if(m_currentBehaviour >= m_dayBehavours.Count)
            {
                ChangePhase();
            }
            
            m_dayBehavours[m_currentBehaviour].InitAction();

            m_currentBehaviour++;
        }

        public void InitMap ()
        {
            float posX = 0.0f;
            float posY = 0.0f;
            
            m_map = new HexaTile[m_mapSizeX, m_mapSizeY];
            
            posX += m_mapOffSet.y;
            
            for (int y = 0; y < m_mapSizeX; y++)
            {
                posY = (y % 2) == 0 ? 0.0f : 0.66f;
                posY += m_mapOffSet.x;

                for (int x = 0; x < m_mapSizeY; x++)
                {
                    m_map[y, x] = TilesPool.Instance.Load();
                    m_map[y, x].SetInfos(new HexaTile.Data(GetRandomType(), x, y, posY, posX));

                    posY += 1.32f;
                }

                posX += 1.0f;
            }
            
            m_bat.SetInitialTile(GetTile(11, 20));

            StartCoroutine(PlaneToNight(false));
        }

        public void AddNewDayElement(GGJMonoBehaviour behaviour)
        {
            m_dayBehavours.Add(behaviour);
        }

        public void AddNewNightElement (GGJMonoBehaviour behaviour)
        {
            m_nightBehavours.Add(behaviour);
        }

        public HexaTile.TYPE GetRandomType ()
        {
            return (HexaTile.TYPE)Random.Range(0, 3);
        }

        public Sprite GetSprite (HexaTile.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.PATHS[type]);
        }

        public Sprite GetAnimalSprite (Bird.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.ANIMALS[type]);
        }

        public Sprite GetFoodSprite(Food.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.FOODS[type]);
        }

        private IEnumerator PlaneToNight (bool doIt = true)
        {
            float lerp = 0.0f;

            Color color = Colors.White;
            
            while(lerp < 0.98f)
            {
                lerp += Time.deltaTime;

                color = m_blackPlane.color;
                color.a = Mathf.Clamp01(lerp);
                m_blackPlane.color = color;

                yield return null;
            }

            m_bat.InitAction();
        }

        private IEnumerator PlaneToDay ()
        {
            float lerp = 1.0f;

            Color color = Colors.White;

            while (lerp > 0.0f)
            {
                lerp -= Time.deltaTime;

                color = m_blackPlane.color;
                color.a = Mathf.Clamp01(lerp);
                m_blackPlane.color = color;

                yield return null;
            }
            
            DayAct();
        }
    }
}