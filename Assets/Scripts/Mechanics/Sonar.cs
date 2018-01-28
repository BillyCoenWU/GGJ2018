namespace GGJ
{
    using UnityEngine;
    using System.Collections;
    public class Sonar : MonoBehaviour
    {
        public SpriteRenderer[] sonarSpriteRenderer;
        public Transform[] sonarTransform;
        public Vector3 maxScale;
        [Range(1,5)]
        public int maxSonarCount;
        public float delayTime;

        public void Init()
        {
            for (int i = 0; i < maxSonarCount; i++)
            {
                StartCoroutine(EscalonarSonar(i, i * delayTime));
            }
        }
        public void Set(Sprite sprite, Vector3 position, Vector3 maxScale, int maxSonarCount)
        {
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

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime;

                transform.localScale = Vector3.Lerp(Constantes.VECTOR_THREE_ZERO, maxScale, Easings.Linear(lerp));

                yield return null;
            }

            yield return null;
        }

    }
}
