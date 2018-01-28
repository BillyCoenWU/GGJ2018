namespace GGJ
{
    using UnityEngine;
    using System.Collections;
    
    public class Bird : GGJMonoBehaviour
    {
        public enum TYPE
        {
            HAWK = 0,
            OWL
        }

        public enum DIRECTION
        {
            UP = 0,
            DOWN,
            LEFT,
            RIGHT,

            RIGHT_UP,
            RIGHT_DOWN,

            LEFT_UP,
            LEFT_DOWN
        }

        private HexaTile m_tile = null;

        private TYPE m_type = TYPE.HAWK;

        [SerializeField]
        private SonarData m_sonarInfos = null;

        [SerializeField]
        private float m_speed = 5.0f;

        [SerializeField]
        private int m_maxActionsPerTurn = 1;
        private int m_movementsLeft = 0;
        
        private SpriteRenderer m_renderer = null;
        
        public void Init (HexaTile tile)
        {
            m_tile = tile;

            m_renderer = GetComponent<SpriteRenderer>();

            m_type = (Random.Range(0, 100) >= 0) ? TYPE.HAWK : TYPE.OWL;

            m_renderer.sprite = Map.Instance.GetAnimalSprite(m_type);

            if (m_type == TYPE.HAWK)
            {
                m_renderer.sortingLayerName = Constantes.DIURNO;
                Map.Instance.AddNewDayElement(this);
            }
            else
            {
                m_renderer.sortingLayerName = Constantes.NOTURNO;
                Map.Instance.AddNewNightElement(this);
            }

            transform.localPosition = tile.data.POSITION;

            gameObject.SetActive(true);
        }

        public override void PlaySonar()
        {
            SonarPool.Instance.Load().Set(m_sonarInfos.sprite, transform.position, m_sonarInfos.maxScale, m_sonarInfos.maxCount,false);
        }

        public override void InitAction()
        {
            m_movementsLeft = m_maxActionsPerTurn;
            
            Act();
        }

        private void Act ()
        {
            m_movementsLeft--;

            if(m_movementsLeft < 0)
            {
                if (m_tile.data.food != null)
                {
                    m_tile.data.food.Eat();
                }
                else if (m_tile.data.bat != null)
                {
                    m_tile.data.bat.TakeDamage();
                }

                if(m_type == TYPE.HAWK)
                {
                    Map.Instance.DayAct();
                }
                else
                {
                    Map.Instance.NightAct();
                }

                return;
            }

            int x = m_tile.data.indexX;
            int y = m_tile.data.indexY;

            HexaTile tile = null;

            DIRECTION direction = DIRECTION.UP;
            
            while (tile == null)
            {
                x = m_tile.data.indexX;
                y = m_tile.data.indexY;
                
                direction = (DIRECTION)Random.Range(0, 8);

                switch (direction)
                {
                    case DIRECTION.UP:
                        y++;
                        break;

                    case DIRECTION.DOWN:
                        y--;
                        break;

                    case DIRECTION.LEFT:
                        x--;
                        break;

                    case DIRECTION.RIGHT:
                        x++;
                        break;

                    case DIRECTION.RIGHT_UP:
                        x++;
                        y++;
                        break;

                    case DIRECTION.RIGHT_DOWN:
                        x++;
                        y--;
                        break;

                    case DIRECTION.LEFT_UP:
                        x--;
                        y++;
                        break;

                    case DIRECTION.LEFT_DOWN:
                        x--;
                        y--;
                        break;
                }

                if (x > 0 && x < Map.Instance.mapSizeX && y > 0 && y < Map.Instance.mapSizeY)
                {
                    tile = Map.Instance.GetTile(y, x);

                    if (tile.data.animal != null)
                    {
                        tile = null;
                    }
                }
            }

            StartCoroutine(Move(tile));
        }

        private IEnumerator Move(HexaTile tile)
        {
            float lerp = 0.0f;
            
            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime * m_speed;

                transform.position = Vector3.Lerp(m_tile.data.POSITION, tile.data.POSITION, lerp);

                yield return null;
            }

            m_tile.data.animal = null;
            m_tile = tile;
            m_tile.data.animal = this;

            Act();
        }
    }
}
