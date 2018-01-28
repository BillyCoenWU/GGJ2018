namespace GGJ
{
    using UnityEngine;
    using System.Collections;
    public class Sonar : MonoBehaviour
    {
        public SpriteRenderer[] sonarSpriteRenderer;
        public Transform[] sonarTransform;
        public Vector3 maxScale;
        [Range(1, 5)]
        public int maxSonarCount;
        public float delayTime;
        public float speed;
        public float alphaSpeed;
        public new Collider2D collider;

        public void Init()
        {
            gameObject.SetActive(true);
            for (int i = 0; i < maxSonarCount; i++)
            {
                sonarTransform[i].gameObject.SetActive(true);
                StartCoroutine(EscalonarSonar(i, i * delayTime));
            }
        }
        public void Set(Sprite sprite, Vector3 position, Vector3 maxScale, int maxSonarCount, bool isOrigin)
        {
            collider.enabled = isOrigin;
            this.maxSonarCount = maxSonarCount;
            this.maxScale = maxScale;
            transform.position = position;
            foreach (var item in sonarSpriteRenderer)
            {
                item.sprite = sprite;
            }
            Init();
        }

        public void Finish()
        {
            foreach (var item in sonarTransform)
            {
                item.transform.localScale = Vector3.zero;
            }
            SonarPool.Instance.Restore(this);
        }

        IEnumerator EscalonarSonar(int i, float delay)
        {
            yield return new WaitForSeconds(delay);

            float lerp = 0.0f;
            float colorLerp = 0.0f;

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime * speed;
                colorLerp += Time.deltaTime * alphaSpeed;
                sonarTransform[i].localScale = Vector3.Lerp(Constantes.VECTOR_THREE_ZERO, maxScale, Easings.QuadraticEaseOut(lerp));

                sonarSpriteRenderer[i].color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Easings.BounceEaseInOut(colorLerp)));

                yield return null;
            }
            sonarTransform[i].gameObject.SetActive(false);

            if (i >= maxSonarCount - 1)
            {
                Finish();
            }
        }

    }
}
