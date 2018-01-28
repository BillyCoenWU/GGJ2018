namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Bat : GGJMonoBehaviour, IUpdate
    {
        [SerializeField]
        private int m_range = 5;

        [SerializeField]
        private float m_stamina = 1.0f;

        [SerializeField]
        private float m_speed = 2.0f;

        private HexaTile m_tile = null;

        public void SetInitialTile (HexaTile tile)
        {
            m_tile = tile;

            transform.localPosition = m_tile.transform.localPosition;
        }

        public void SetTarget ()
        {
            CameraControl.Instance.SetTargetToFollow(this);
        }

        public override void InitAction()
        {
            Game.update += CustomUpdate;
        }

        public void CustomUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit2D raycastHit2D = Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f, Constantes.FOWARD, float.MaxValue, 1<<8);

                if (raycastHit2D.collider != null)
                {
                    HexaTile tile = raycastHit2D.collider.GetComponent<HexaTile>();
                    
                    if (tile.data.indexX != m_tile.data.indexX || tile.data.indexY != m_tile.data.indexY)
                    {
                        if (tile.data.indexX >= m_tile.data.indexX - 5 && tile.data.indexX <= m_tile.data.indexX + 5)
                        {
                            if (tile.data.indexY >= m_tile.data.indexY - 5 && tile.data.indexY <= m_tile.data.indexY + 5)
                            {
                                if (tile.data.obstacle == null)
                                {
                                    StartCoroutine(MoveToTile(tile));
                                }
                            }
                            else
                            {
                                Debug.Log(4);
                            }
                        }
                        else
                        {
                            Debug.Log(5);
                        }
                    }
                    
                }
            }
        }

        private IEnumerator MoveToTile (HexaTile tile)
        {
            Game.update -= CustomUpdate;

            float lerp = 0.0f;

            while(lerp <= 1.0f)
            {
                lerp += Time.deltaTime * m_speed;

                transform.localPosition = Vector2.Lerp(m_tile.data.POSITION, tile.data.POSITION, lerp);

                yield return null;
            }

            m_tile = tile;

            if(m_tile.data.food != null)
            {
                m_tile.data.food.Eat();
            }

            Map.Instance.NightAct();
        }
    }
}
