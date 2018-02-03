namespace GGJ
{
    using RGSMS.Sound;
    using UnityEngine;
    using System.Collections;

    public class Bat : GGJMonoBehaviour, IUpdate
    {
        [SerializeField]
        private SonarData m_sonarInfos = null;

        [Space(5.0f)]

        [SerializeField]
        private GameAudioObject m_wing = null;
        [SerializeField]
        private GameAudioObject m_eat = null;

        [Space(5.0f)]

        [SerializeField]
        private int life = 3;
        
        private int m_range = 1;
        public int range
        {
            set
            {
                m_range = value;
            }
        }

        [SerializeField]
        private int m_foods = 10;


        [SerializeField]
        private float m_speed = 2.0f;

        private HexaTile m_tile = null;

        public void SetInitialTile (HexaTile tile)
        {
            m_tile = tile;

            m_tile.data.bat = this;

            m_wing.Play(true);

            InGameUI.Instance.SetLives(life);
            InGameUI.Instance.SetFood(m_foods);

            transform.localPosition = m_tile.transform.localPosition;
        }

        public void SetTarget ()
        {
            CameraControl.Instance.SetTargetToFollow(this);
        }

        public override void PlaySonar()
        {
            SonarPool.Instance.Load().Set(m_sonarInfos.sprite, transform.localPosition, m_sonarInfos.maxScale, m_sonarInfos.maxCount, true);

            Game.update += CustomUpdate;
        }

        public override void InitAction()
        {
            InGameUI.Instance.ActiveSlider();
        }

        public void TakeDamage ()
        {
            life--;

            InGameUI.Instance.SetLives(life);

            if (life <= 0)
            {
                InGameUI.Instance.GameOver();
            }
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
                        if (tile.data.indexX >= m_tile.data.indexX - m_range && tile.data.indexX <= m_tile.data.indexX + m_range)
                        {
                            if (tile.data.indexY >= m_tile.data.indexY - m_range && tile.data.indexY <= m_tile.data.indexY + m_range)
                            {
                                if (tile.data.obstacle == null)
                                {
                                    StartCoroutine(MoveToTile(tile));
                                }
                            }
                            else
                            {
#if UNITY_EDITOR
                                Debug.Log("Clicou Longe");
                                Debug.Log("COLOCAR SOM DE ERRO AQUI!");
#endif
                            }
                        }
                        else
                        {
#if UNITY_EDITOR
                            Debug.Log("Clicou Longe");
                            Debug.Log("COLOCAR SOM DE ERRO AQUI!");
#endif
                        }
                    }
                    
                }
            }
        }

        private void FinishAction ()
        {
            if (m_tile.data.morceguita != null)
            {
                InGameUI.Instance.EndGame();
                return;
            }
            else
            {
                if (m_tile.data.animal != null)
                {
                    TakeDamage();

                    if (life <= 0)
                    {
                        return;
                    }
                }
                
                if (m_tile.data.food != null)
                {
                    m_eat.Play();
                    m_foods += m_tile.data.food.Eat();
                }

                m_foods--;

                m_foods = Mathf.Clamp(m_foods, 0, 100);

                InGameUI.Instance.SetFood(m_foods);

                if (m_foods <= 0)
                {
                    TakeDamage();
                }

                if(life <= 0)
                {
                    return;
                }

                Map.Instance.NightAct();
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

            m_tile.data.bat = null;
            m_tile = tile;
            m_tile.data.bat = this;

            FinishAction();
        }
    }
}
