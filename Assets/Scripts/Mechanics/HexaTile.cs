namespace GGJ
{
    using UnityEngine;

    public class HexaTile : MonoBehaviour
    {
        public enum TYPE
        {
            GRASS = 0,
            GROUND,
            RIVER,

            ROCK,
            TREE,

            ANIMAL,
            FOOD
        }

        [System.Serializable]
        public class Data
        {
            public Data (TYPE type, int x, int y, float posX, float posY)
            {
                m_type = type;

                m_indexX = x;
                m_indexY = y;

                POSITION = new Vector2(posX, posY);
            }

            public readonly Vector2 POSITION = Constantes.VECTOR_TWO_ZERO;

            public Bat bat = null;

            public Food food = null;

            public Bird animal = null;

            public Obstacle obstacle = null;
            
            private TYPE m_type = TYPE.GRASS;
            public TYPE type
            {
                set
                {
                    m_type = value;
                }

                get
                {
                    return m_type;
                }
            }

            private int m_indexX = 0;
            public int indexX
            {
                get
                {
                    return m_indexX;
                }
            }

            private int m_indexY = 0;
            public int indexY
            {
                get
                {
                    return m_indexY;
                }
            }
        }

        private SpriteRenderer m_renderer = null;
        
        private Data m_data = null;
        public Data data
        {
            get
            {
                return m_data;
            }
        }
        
        public void SetInfos (Data data)
        {
            m_data = data;

            transform.localPosition = m_data.POSITION;

            if (m_data.type == TYPE.GROUND || m_data.type == TYPE.GRASS)
            {
                int random = Random.Range(0, 100);

                switch(Random.Range(0, 3))
                {
                    case 0:
                        if (random > 50)
                        {
                            if (Map.Instance.CanSpawnObstacle())
                            {
                                m_data.obstacle = ObstaclesPool.Instance.Load();
                                m_data.obstacle.SetType(this);
                            }
                        }
                        break;

                    case 1:
                        if (random > 85)
                        {
                            if (Map.Instance.CanSpawnEnemy())
                            {
                                m_data.animal = AnimalsPool.Instance.Load();
                                m_data.animal.Init(this);
                            }
                        }
                        break;

                    case 2:
                        if (random > 90)
                        {
                            if (Map.Instance.CanSpawnFood())
                            {
                                m_data.food = FruitPool.Instance.Load();
                                m_data.food.Init(this);
                            }
                        }
                        break;
                }
            }
            
            m_renderer = GetComponent<SpriteRenderer>();

            m_renderer.sprite = Map.Instance.GetSprite(m_data.type);

            gameObject.SetActive(true);
        }

        /*
        public void SetColor ()
        {
            switch (m_data.type)
            {
                default:
                    break;

                case TYPE.ANIMAL:
                    m_renderer.color = Colors.DarkRed;
                    break;

                case TYPE.FOOD:
                    m_renderer.color = Colors.YellowGreen;
                    break;

                case TYPE.TREE:
                    m_renderer.color = Colors.ForestGreen;
                    break;

                case TYPE.RIVER:
                    m_renderer.color = Colors.LightBlue;
                    break;

                case TYPE.ROCK:
                    m_renderer.color = Colors.Brown;
                    break;
            }
        }
        */
    }
}