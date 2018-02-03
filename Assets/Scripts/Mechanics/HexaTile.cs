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

            public Morceguita morceguita = null;

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
            
            m_renderer = GetComponent<SpriteRenderer>();

            m_renderer.sprite = Map.Instance.GetSprite(m_data.type);

            gameObject.SetActive(true);
        }
    }
}