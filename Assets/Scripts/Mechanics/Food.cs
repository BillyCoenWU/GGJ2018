namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Food : GGJMonoBehaviour
    {
        public enum TYPE
        {
            FRUIT_ONE = 0,
            FRUIT_TWO,
            FRUIT_THREE,

            MOTH
        }

        private TYPE m_type = TYPE.FRUIT_ONE;

        private SpriteRenderer m_renderer = null;

        [SerializeField]
        private SonarData m_sonarInfos = null;

        private HexaTile m_tile = null;

        [SerializeField]
        private int m_maxActionsPerTurn = 1;
        private int m_movementsLeft = 0;

        [SerializeField]
        private int m_fruitFood = 1;
        [SerializeField]
        private int m_mothFood = 1;
        
        [SerializeField]
        private float m_speed = 5.0f;
        
        public int Eat ()
        {
            m_tile.data.food = null;
            m_tile = null;

            if(m_type == TYPE.MOTH)
            {
                Map.Instance.RemoveDayElement(this);
            }

            Map.Instance.AddNewFoodToRespawn(m_type);

            FruitPool.Instance.Restore(this);

            return m_type != TYPE.MOTH ? m_fruitFood : m_mothFood;
        }

        private void Act ()
        {
            m_movementsLeft--;

            if (m_movementsLeft < 0)
            {
                if (m_tile.data.food != null)
                {
                    m_tile.data.food.Eat();
                }

                Map.Instance.NightAct();

                return;
            }

            int x = m_tile.data.indexX;
            int y = m_tile.data.indexY;

            HexaTile tile = null;

            Bird.DIRECTION direction = Bird.DIRECTION.UP;

            while (tile == null)
            {
                x = m_tile.data.indexX;
                y = m_tile.data.indexY;

                direction = (Bird.DIRECTION)Random.Range(0, 8);

                switch (direction)
                {
                    case Bird.DIRECTION.UP:
                        y++;
                        break;

                    case Bird.DIRECTION.DOWN:
                        y--;
                        break;

                    case Bird.DIRECTION.LEFT:
                        x--;
                        break;

                    case Bird.DIRECTION.RIGHT:
                        x++;
                        break;

                    case Bird.DIRECTION.RIGHT_UP:
                        x++;
                        y++;
                        break;

                    case Bird.DIRECTION.RIGHT_DOWN:
                        x++;
                        y--;
                        break;

                    case Bird.DIRECTION.LEFT_UP:
                        x--;
                        y++;
                        break;

                    case Bird.DIRECTION.LEFT_DOWN:
                        x--;
                        y--;
                        break;
                }

                if (x > 0 && x < Map.Instance.mapSizeX && y > 0 && y < Map.Instance.mapSizeY)
                {
                    tile = Map.Instance.GetTile(y, x);

                    if (tile.data.animal != null || tile.data.bat != null || tile.data.morceguita != null)
                    {
                        tile = null;
                    }
                }
            }

            StartCoroutine(Move(tile));
        }

        public void Init (HexaTile tile)
        {
            m_tile = tile;

            m_tile.data.food = this;

            m_type = (Random.Range(0, 100) > 90) ? TYPE.MOTH : (TYPE)Random.Range(0, 3);

            if (m_type == TYPE.MOTH)
            {
                Map.Instance.AddNewDayElement(this);
            }
            
            switch (m_type)
            {
                case TYPE.FRUIT_ONE:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(0);
                    break;

                case TYPE.FRUIT_TWO:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(1);
                    break;

                case TYPE.FRUIT_THREE:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(2);
                    break;

                case TYPE.MOTH:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(3);
                    break;
            }
            
            m_renderer = GetComponent<SpriteRenderer>();

            m_renderer.sprite = Map.Instance.GetFoodSprite(m_type);

            transform.position = tile.data.POSITION;

            gameObject.SetActive(true);
        }

        public override void PlaySonar ()
        {
            SonarPool.Instance.Load().Set(m_sonarInfos.sprite, transform.position, m_sonarInfos.maxScale, m_sonarInfos.maxCount, false);
        }

        public override void InitAction ()
        {
            m_movementsLeft = m_maxActionsPerTurn;

            Act();
        }

        public void Respawn (HexaTile tile, TYPE type)
        {
            m_tile = tile;

            m_tile.data.food = this;

            m_type = type;
            
            switch (m_type)
            {
                case TYPE.FRUIT_ONE:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(0);
                    break;

                case TYPE.FRUIT_TWO:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(1);
                    break;

                case TYPE.FRUIT_THREE:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(2);
                    break;

                case TYPE.MOTH:
                    m_sonarInfos.sprite = Map.Instance.GetSonarSprite(3);
                    break;
            }
            
            if (m_type == TYPE.MOTH)
            {
                Map.Instance.AddNewDayElement(this);
            }

            if (m_renderer == null)
            {
                m_renderer = GetComponent<SpriteRenderer>();
            }

            m_renderer.sprite = Map.Instance.GetFoodSprite(m_type);

            transform.position = tile.data.POSITION;

            gameObject.SetActive(true);
        }

        private IEnumerator Move (HexaTile tile)
        {
            float lerp = 0.0f;

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime * m_speed;

                transform.position = Vector3.Lerp(m_tile.data.POSITION, tile.data.POSITION, lerp);

                yield return null;
            }

            m_tile.data.food = null;
            m_tile = tile;
            m_tile.data.food = this;

            Act();
        }
    }
}
