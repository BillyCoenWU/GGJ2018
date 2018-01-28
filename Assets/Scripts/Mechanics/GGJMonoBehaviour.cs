namespace GGJ
{
    using UnityEngine;

    public abstract class GGJMonoBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public class SonarData
        {
            public int maxCount = 0;

            public Sprite sprite = null;

            public Vector3 maxScale = Constantes.VECTOR_THREE_ONE;
        }

        public abstract void PlaySonar();
        public abstract void InitAction();
    }
}
