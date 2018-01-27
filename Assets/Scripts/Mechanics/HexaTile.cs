namespace GGJ
{
    using UnityEngine;
    using UnityEngine.U2D;

    public class HexaTile : MonoBehaviour
    {
        public enum TYPE
        {
            NORMAL = 0,

            ROCK,
            TREE,
            RIVER,

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
            
            private TYPE m_type = TYPE.NORMAL;
            public TYPE type
            {
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

        private Data m_data = null;
        public Data data
        {
            get
            {
                return m_data;
            }
        }
        
        private SpriteRenderer m_renderer = null;
        
        public void SetInfos (Data data)
        {
            m_data = data;

            m_renderer = GetComponent<SpriteRenderer>();

            SetColor();

            switch(m_data.type)
            {

            }

            /*
            m_renderer.sprite = Resources.Load<SpriteAtlas>(Constantes.ATLAS_PATH).GetSprite(Constantes.PATHS[m_data.type]);
            */

            transform.localPosition = m_data.POSITION;

            gameObject.SetActive(true);
        }

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
    }
}