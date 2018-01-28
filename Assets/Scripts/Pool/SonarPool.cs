namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class SonarPool : Singleton<SonarPool>
    {
        [SerializeField]
        private int m_count = 3;

        [SerializeField]
        private Object m_reference = null;

        private Queue<Sonar> m_sonar = null;

        private void Awake()
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            InitPool();
        }

        private void InitPool()
        {
            m_sonar = new Queue<Sonar>();

            for (int i = 0; i < m_count; i++)
            {
                Restore(GetSonar());
            }
        }

        public Sonar Load()
        {
            return m_sonar.Count > 0 ? m_sonar.Dequeue() : GetSonar();
        }

        public void Restore(Sonar sonar)
        {
            m_sonar.Enqueue(sonar);

            sonar.gameObject.SetActive(false);
            sonar.transform.SetParent(transform);
        }

        private Sonar GetSonar()
        {
            return CreateByReference().GetComponent<Sonar>();
        }

        private GameObject CreateByReference()
        {
            return (GameObject)Instantiate(m_reference, Constantes.VECTOR_THREE_ZERO, Constantes.IDENTITY);
        }
    }
}