namespace GGJ
{
    using UnityEngine;
    using UnityEngine.U2D;

    public class Map : Singleton<Map>
    {
        private SpriteAtlas m_mapAtlas = null;
        
        [SerializeField]
        private int m_foodCount = 0;

        [SerializeField]
        private int m_enemysCount = 0;

        [SerializeField]
        private int m_obstaclesCount = 0;

        [Space(5.0f)]

        [SerializeField]
        private Bat m_bat = null;

        [Space(5.0f)]

        [SerializeField]
        private Vector2 m_mapOffSet = Constantes.VECTOR_TWO_ZERO;
        
        private HexaTile[,] m_map = null;
        public HexaTile GetTile (int x, int y)
        {
            return m_map[x,y];
        }

        private void Awake()
        {
            Instance = this;

            m_mapAtlas = Resources.Load<SpriteAtlas>(Constantes.ATLAS_PATH);
        }

        private void Start()
        {
            InitMap();
        }

        public void InitMap ()
        {
            float posX = 0.0f;
            float posY = 0.0f;
            
            m_map = new HexaTile[23, 40];
            
            posX += m_mapOffSet.y;
            
            for (int y = 0; y < 23; y++)
            {
                posY = (y % 2) == 0 ? 0.0f : 0.66f;
                posY += m_mapOffSet.x;

                for (int x = 0; x < 40; x++)
                {
                    m_map[y, x] = TilesPool.Instance.Load();
                    m_map[y, x].SetInfos(new HexaTile.Data(GetRandomType(), x, y, posY, posX));

                    posY += 1.32f;
                }

                posX += 1.0f;
            }


            m_bat.SetInitialTile(GetTile(11, 20));
        }

        public HexaTile.TYPE GetRandomType ()
        {
            return (HexaTile.TYPE)Random.Range(0, 6);
        }

        public Sprite GetSprite (HexaTile.TYPE type)
        {
            return m_mapAtlas.GetSprite(Constantes.PATHS[type]);
        }
    }
}