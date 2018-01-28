namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class ObstaclesPool : Singleton<ObstaclesPool>
    {
        [SerializeField]
        private int m_count = 20;

        [SerializeField]
        private Object m_reference = null;

        private Queue<Obstacle> m_obstacles = null;

        private void Awake()
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            InitPool();
        }

        private void InitPool()
        {
            m_obstacles = new Queue<Obstacle>();

            for (int i = 0; i < m_count; i++)
            {
                Restore(GetObstacle());
            }
        }

        public Obstacle Load()
        {
            return m_obstacles.Count > 0 ? m_obstacles.Dequeue() : GetObstacle();
        }

        public void Restore(Obstacle obstacle)
        {
            m_obstacles.Enqueue(obstacle);

            obstacle.gameObject.SetActive(false);
            obstacle.transform.SetParent(transform);
        }

        private Obstacle GetObstacle()
        {
            return CreateByReference().GetComponent<Obstacle>();
        }

        private GameObject CreateByReference()
        {
            return (GameObject)Instantiate(m_reference, Constantes.VECTOR_THREE_ZERO, Constantes.IDENTITY);
        }
    }
}
