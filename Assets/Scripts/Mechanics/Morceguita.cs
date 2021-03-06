﻿namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Morceguita : GGJMonoBehaviour
    {
        [SerializeField]
        private SonarData m_sonarInfos = null;

        private HexaTile m_tile = null;

        [SerializeField]
        private int m_maxActionsPerTurn = 1;
        private int m_movementsLeft = 0;

        [SerializeField]
        private float m_speed = 5.0f;

        public void Init(HexaTile tile)
        {
            m_tile = tile;
            m_tile.data.morceguita = this;

            transform.position = m_tile.data.POSITION;
            
            Map.Instance.AddNewNightElement(this);
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

        private void Act()
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

                    if (tile.data.animal != null || tile.data.bat != null)
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

            m_tile.data.morceguita = null;
            m_tile = tile;
            m_tile.data.morceguita = this;

            Act();
        }
    }
}
