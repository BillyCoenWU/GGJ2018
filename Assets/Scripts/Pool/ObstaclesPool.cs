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

        private Queue<Obstacle> m_tiles = null;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitPool();
        }

        private void InitPool()
        {
            m_tiles = new Queue<Obstacle>();

            for (int i = 0; i < m_count; i++)
            {
                Restore(GetObstacle());
            }
        }

        public Obstacle Load()
        {
            return m_tiles.Count > 0 ? m_tiles.Dequeue() : GetObstacle();
        }

        public void Restore(Obstacle tile)
        {
            m_tiles.Enqueue(tile);

            tile.gameObject.SetActive(false);
            tile.transform.SetParent(transform);
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
