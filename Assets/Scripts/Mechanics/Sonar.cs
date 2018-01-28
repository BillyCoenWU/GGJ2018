namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Sonar : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer[] m_sonarSpriteRenderer = null;

        [SerializeField]
        private Transform[] m_sonarTransform = null;

        [SerializeField]
        private Collider2D m_collider2D = null;

        [SerializeField]
        private Vector3 m_maxScale = Constantes.VECTOR_THREE_ZERO;

        [SerializeField, Range(1, 5)]
        private int m_maxSonarCount = 1;

        [SerializeField]
        private float m_delayTime = 0.2f;

        [SerializeField]
        private float m_speed = 0.2f;

        [SerializeField]
        private float m_alphaSpeed = 0.5f;

        private bool m_isOrigin = true;
        public bool isOrigin
        {
            get
            {
                return m_isOrigin;
            }
        }
        
        public void Set(Sprite sprite, Vector3 position, Vector3 maxScale, int maxSonarCount, bool _isOrigin)
        {
            m_isOrigin = _isOrigin;

            m_collider2D.enabled = m_isOrigin;

            m_maxScale = maxScale;
            m_maxSonarCount = maxSonarCount;

            transform.position = position;

            for (int i = 0; i < maxSonarCount; i++)
            {
                m_sonarSpriteRenderer[i].sprite = sprite;
            }
            
            Init();
        }

        private void Init()
        {
            gameObject.SetActive(true);

            for (int i = 0; i < m_maxSonarCount; i++)
            {
                m_sonarTransform[i].gameObject.SetActive(true);

                StartCoroutine(EscalonarSonar(i, i * m_delayTime));
            }
        }

        private void Finish()
        {
            for (int i = 0; i < m_maxSonarCount; i++)
            {
                m_sonarSpriteRenderer[i].color = Color.white;
                m_sonarTransform[i].localScale = Constantes.VECTOR_THREE_ZERO;
            }
            
            SonarPool.Instance.Restore(this);
        }

        private IEnumerator EscalonarSonar(int i, float delay)
        {
            yield return new WaitForSeconds(delay);

            float lerp = 0.0f;
            float colorLerp = 0.0f;

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime * m_speed;
                colorLerp += Time.deltaTime * m_alphaSpeed;

                m_sonarTransform[i].localScale = Vector3.Lerp(Constantes.VECTOR_THREE_ZERO, m_maxScale, Easings.QuadraticEaseOut(lerp));

                if (m_sonarSpriteRenderer[i].color.a > 0.1f)
                {
                    m_sonarSpriteRenderer[i].color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1.0f, 0.0f, Easings.CircularEaseOut(colorLerp)));
                }
                else
                {
                    m_sonarSpriteRenderer[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
                
                yield return null;
            }

            m_sonarTransform[i].gameObject.SetActive(false);

            if (i >= (m_maxSonarCount-1))
            {
                Finish();
            }
        }
    }
}
