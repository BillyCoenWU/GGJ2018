namespace GGJ
{
    using RGSMS.Sound;
    using UnityEngine;
    using UnityEngine.U2D;
    using System.Collections;
    using System.Collections.Generic;

    public class Map : Singleton<Map>
    {
        [System.Serializable]
        public class FoodRespawnData
        {
            public int turns = 3;
            public Food.TYPE type = Food.TYPE.FRUIT_ONE;

            public bool Respawn ()
            {
                turns--;

                return (turns <= 0);
            }
        }

        public enum PHASE
        {
            NIGHT,
            DAY
        }

        private PHASE m_phase = PHASE.NIGHT;
        public PHASE phase
        {
            get
            {
                return m_phase;
            }
        }
        
        private SpriteAtlas m_mapAtlas = null;
        private SpriteAtlas m_outlinesAtlas = null;

        private List<GGJMonoBehaviour> m_dayBehavours = null;
        private List<GGJMonoBehaviour> m_nightBehavours = null;

        private HexaTile[,] m_map = null;
        public HexaTile GetTile(int x, int y)
        {
            return m_map[x, y];
        }

        [SerializeField]
        private bool m_respawnFood = true;

        [SerializeField]
        private int m_turnsToRespawn = 5;
      
        [Space (5.0f)]

        private int m_turns = 0;

        [SerializeField]
        private int m_foodCount = 0;

        [SerializeField]
        private int m_enemysCount = 0;

        [SerializeField]
        private int m_obstaclesCount = 0;

        private int m_currentBehaviour = 0;

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
        public Bat bat
        {
            get
            {
                return m_bat;
            }
        }

        public List<FoodRespawnData> m_foodsToRespawn = null;

        [SerializeField]
        private Morceguita m_batima = null;

        [SerializeField]
        private SpriteRenderer m_blackPlane = null;

        [Space(5.0f)]
        
        [SerializeField]
        private Vector2 m_mapOffSet = Constantes.VECTOR_TWO_ZERO;

        [Space(5.0f)]

        [SerializeField]
        private GameAudioObject m_bgmDay = null;

        [SerializeField]
        private GameAudioObject m_bgmNight = null;

        private void Awake ()
        {
            Instance = this;

            m_foodsToRespawn = new List<FoodRespawnData>();

            m_dayBehavours = new List<GGJMonoBehaviour>();
            m_nightBehavours = new List<GGJMonoBehaviour>();

            m_mapAtlas = Resources.Load<SpriteAtlas>(Constantes.ATLAS_PATH);
            m_outlinesAtlas = Resources.Load<SpriteAtlas>(Constantes.SONAR_PATH);
        }

        private void Start ()
        {
            InGameUI.Instance.SetTime(0);

            InitMap();
        }

        public void DayAct ()
        {
            if(m_currentBehaviour >= m_dayBehavours.Count)
            {
                ChangePhase();
                return;
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

            int random = Random.Range(0, 4);

            switch(random)
            {
                case 0:
                    m_batima.Init(GetTile(1, 1));
                    break;

                case 1:
                    m_batima.Init(GetTile(21, 1));
                    break;

                case 2:
                    m_batima.Init(GetTile(21, 37));
                    break;

                case 3:
                    m_batima.Init(GetTile(1, 38));
                    break;
            }

            HexaTile tile = null;
            Obstacle obstacle = null;

            for(int i = 0; i < m_obstaclesCount; i++)
            {
                obstacle = ObstaclesPool.Instance.Load();

                while (tile == null)
                {
                    tile = GetTile(Random.Range(0, mapSizeY), Random.Range(0, mapSizeX));

                    if (tile.data.type == HexaTile.TYPE.GRASS || tile.data.type == HexaTile.TYPE.GROUND)
                    {
                        if (tile.data.bat != null || tile.data.obstacle != null)
                        {
                            tile = null;
                        }
                    }
                }

                obstacle.SetType(tile);
                tile = null;
            }

            Food food = null;

            for (int i = 0; i < m_foodCount; i++)
            {
                food = FruitPool.Instance.Load();

                while (tile == null)
                {
                    tile = GetTile(Random.Range(0, mapSizeY), Random.Range(0, mapSizeX));

                    if (tile.data.type == HexaTile.TYPE.GRASS || tile.data.type == HexaTile.TYPE.GROUND)
                    {
                        if (tile.data.bat != null || tile.data.obstacle != null || tile.data.food != null)
                        {
                            tile = null;
                        }
                    }
                }

                food.Init(tile);
                tile = null;
            }
            
            Bird bird = null;

            for (int i = 0; i < m_enemysCount; i++)
            {
                bird = AnimalsPool.Instance.Load();

                while (tile == null)
                {
                    tile = GetTile(Random.Range(0, mapSizeY), Random.Range(0, mapSizeX));

                    if (tile.data.type == HexaTile.TYPE.GRASS || tile.data.type == HexaTile.TYPE.GROUND)
                    {
                        if (tile.data.bat != null || tile.data.obstacle != null || tile.data.food != null || tile.data.animal != null)
                        {
                            tile = null;
                        }
                    }
                }

                bird.Init(tile);
                tile = null;
            }

            //m_bgmDay.Play(true);
            m_bgmNight.Play(true);

            StartCoroutine(PlaneToNight());
        }

        public void NightAct ()
        {
            if (m_currentBehaviour >= m_nightBehavours.Count)
            {
                ChangePhase();
                return;
            }

            m_nightBehavours[m_currentBehaviour].InitAction();

            m_currentBehaviour++;
        }

        public void ChangePhase ()
        {
            m_phase = m_phase == PHASE.DAY ? PHASE.NIGHT : PHASE.DAY;

            m_currentBehaviour = 0;

            if (m_phase == PHASE.DAY)
            {
                m_bgmDay.UnPause();
                m_bgmNight.Pause();

                m_turns++;
                InGameUI.Instance.SetTime(m_turns);

                if (m_respawnFood)
                {
                    int count = m_foodsToRespawn.Count;
                    for (int i = count - 1; i >= 0; i--)
                    {
                        if (m_foodsToRespawn[i].Respawn())
                        {
                            Food food = FruitPool.Instance.Load();

                            HexaTile tile = null;

                            int x = 0;
                            int y = 0;

                            while (tile == null)
                            {
                                x = Random.Range(0, m_mapSizeX);
                                y = Random.Range(0, m_mapSizeY);

                                if (x > 0 && x < Map.Instance.mapSizeX && y > 0 && y < Map.Instance.mapSizeY)
                                {
                                    tile = Map.Instance.GetTile(y, x);

                                    if (tile.data.animal != null || tile.data.bat != null || tile.data.food != null || tile.data.obstacle != null || tile.data.morceguita != null)
                                    {
                                        tile = null;
                                    }
                                }
                            }

                            food.Respawn(tile, m_foodsToRespawn[i].type);

                            m_foodsToRespawn.RemoveAt(i);
                        }
                    }
                }

                StartCoroutine(PlaneToDay());
            }
            else
            {
                m_bgmDay.Pause();
                m_bgmNight.UnPause();

                StartCoroutine(PlaneToNight());
            }
        }

        public bool CanSpawnFood ()
        {
            m_foodCount--;

            return (m_foodCount >= 0);
        }

        public bool CanSpawnEnemy ()
        {
            m_enemysCount--;

            return (m_enemysCount >= 0);
        }

        public bool CanSpawnObstacle ()
        {
            m_obstaclesCount--;

            return (m_obstaclesCount >= 0);
        }

        public void AddNewFoodToRespawn (Food.TYPE type)
        {
            if(!m_respawnFood)
            {
                return;
            }

            FoodRespawnData data = new FoodRespawnData();
            data.type = type;
            data.turns = m_turnsToRespawn;

            m_foodsToRespawn.Add(data);
        }
        
        public void AddNewDayElement (GGJMonoBehaviour behaviour)
        {
            m_dayBehavours.Add(behaviour);
        }

        public void AddNewNightElement (GGJMonoBehaviour behaviour)
        {
            m_nightBehavours.Add(behaviour);
        }

        public void RemoveDayElement (GGJMonoBehaviour behaviour)
        {
            m_dayBehavours.Remove(behaviour);
        }

        public HexaTile.TYPE GetRandomType ()
        {
            return (HexaTile.TYPE)Random.Range(0, 3);
        }

        public Sprite GetSonarSprite (int index)
        {
            return m_outlinesAtlas.GetSprite(Constantes.OUTLINES[index]);
        }

        public Sprite GetFoodSprite (Food.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.FOODS[type]);
        }

        public Sprite GetSprite (HexaTile.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.PATHS[type]);
        }

        public Sprite GetAnimalSprite (Bird.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.ANIMALS[type]);
        }

        private IEnumerator PlaneToDay ()
        {
            float lerp = 1.0f;

            Color color = Color.white;

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

        private IEnumerator PlaneToNight ()
        {
            float lerp = 0.0f;

            Color color = Color.white;
            
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
    }
}