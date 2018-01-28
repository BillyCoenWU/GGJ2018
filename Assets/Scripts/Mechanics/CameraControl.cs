namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class CameraControl : Singleton<CameraControl>, ILateUpdate
    {
        [SerializeField]
        private GGJMonoBehaviour m_target = null;

        private Vector3 m_position = Constantes.VECTOR_THREE_ZERO;

        private void Awake()
        {
            Instance = this;
        }

        private void Start ()
        {
            m_position.z = transform.localPosition.z;

            Game.lateUpdate += CustomLateUpdate;
        }

        private void OnDisable()
        {
            Game.lateUpdate -= CustomLateUpdate;
        }

        public void CustomLateUpdate()
        {
            m_position.x = Mathf.Clamp(m_target.transform.position.x, 0.0f, 35.75f);
            m_position.y = Mathf.Clamp(m_target.transform.position.y, 0.0f, 13.4f);

            transform.localPosition = m_position;
        }

        public void SetTargetToFollow (GGJMonoBehaviour target)
        {
            Game.lateUpdate -= CustomLateUpdate;

            m_target = target;

            StartCoroutine(MoveTo());
        }
        
        private IEnumerator MoveTo ()
        {
            float lerp = 0.0f;

            Vector3 position = Constantes.VECTOR_THREE_ZERO;
            Vector3 startPosition = transform.localPosition;
            Vector3 finalPosition = m_target.transform.localPosition;
            finalPosition.z = startPosition.z;

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime;

                position = Vector3.Lerp(startPosition, finalPosition, Easings.QuadraticEaseOut(lerp));
                position.x = Mathf.Clamp(position.x, 0.0f, 35.75f);
                position.y = Mathf.Clamp(position.y, 0.0f, 13.4f);

                transform.localPosition = position;

                yield return null;
            }

            Game.lateUpdate += CustomLateUpdate;
            m_target.InitAction();
        }
    }
}
