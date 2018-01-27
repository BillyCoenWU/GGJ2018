namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class CameraControl : MonoBehaviour, ILateUpdate
    {
        [SerializeField]
        private GGJMonoBehaviour m_target = null;

        private Vector3 m_position = Constantes.VECTOR_THREE_ZERO;

        private void Start ()
        {
            m_position.z = transform.localPosition.z;

            Game.lateUpdate += CustomLateUpdate;
        }

        public void CustomLateUpdate()
        {
            m_position.x = m_target.transform.position.x;
            m_position.y = m_target.transform.position.y;

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

            Vector3 startPosition = transform.localPosition;
            Vector3 finalPosition = m_target.transform.localPosition;
            finalPosition.z = startPosition.z;

            while (lerp < 1.0f)
            {
                lerp += Time.deltaTime;

                transform.localPosition = Vector3.Lerp(startPosition, finalPosition, Easings.QuadraticEaseOut(lerp));

                yield return null;
            }

            Game.lateUpdate += CustomLateUpdate;
            m_target.InitAction();
        }
    }
}
