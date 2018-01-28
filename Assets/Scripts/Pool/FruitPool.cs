namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class FruitPool : Singleton<FruitPool>
    {
        [SerializeField]
        private int m_count = 0;

        [SerializeField]
        private Object m_reference = null;

        private Queue<Food> m_foods = null;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitPool();
        }

        private void InitPool()
        {
            m_foods = new Queue<Food>();

            for (int i = 0; i < m_count; i++)
            {
                Restore(GetFood());
            }
        }

        public Food Load()
        {
            return m_foods.Count > 0 ? m_foods.Dequeue() : GetFood();
        }

        public void Restore(Food food)
        {
            m_foods.Enqueue(food);

            food.gameObject.SetActive(false);
            food.transform.SetParent(transform);
        }

        private Food GetFood()
        {
            return CreateByReference().GetComponent<Food>();
        }

        private GameObject CreateByReference()
        {
            return (GameObject)Instantiate(m_reference, Constantes.VECTOR_THREE_ZERO, Constantes.IDENTITY);
        }
    }
}
