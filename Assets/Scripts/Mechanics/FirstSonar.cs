namespace GGJ
{
    using UnityEngine;

    public class FirstSonar : MonoBehaviour
    {
        [SerializeField]
        private Sonar m_sonar = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_sonar.isOrigin)
            {
                if(Vector2.Distance(other.transform.position, m_sonar.transform.position) > InGameUI.Instance.sonarRanger)
                {
                    return;
                }
            }

            other.GetComponent<GGJMonoBehaviour>().PlaySonar();
        }
    }
}
