namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class AnimalsPool : Singleton<AnimalsPool>
    {
        [SerializeField]
        private int m_count = 0;

        [SerializeField]
        private Object m_reference = null;

        private Queue<Bird> m_birds = null;

        private void Awake()
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            InitPool();
        }

        private void InitPool()
        {
            m_birds = new Queue<Bird>();

            for (int i = 0; i < m_count; i++)
            {
                Restore(GetBird());
            }
        }

        public Bird Load()
        {
            return m_birds.Dequeue();
            //return m_birds.Count > 0 ? m_birds.Dequeue() : GetBird();
        }

        public void Restore(Bird bird)
        {
            m_birds.Enqueue(bird);

            bird.gameObject.SetActive(false);
            bird.transform.SetParent(transform);
        }

        private Bird GetBird()
        {
            return CreateByReference().GetComponent<Bird>();
        }

        private GameObject CreateByReference()
        {
            return (GameObject)Instantiate(m_reference, Constantes.VECTOR_THREE_ZERO, Constantes.IDENTITY);
        }
    }
}
