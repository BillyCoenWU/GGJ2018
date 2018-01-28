namespace GGJ
{
    using UnityEngine;

    public class Food : GGJMonoBehaviour
    {
        public enum TYPE
        {
            FRUIT_ONE = 0,
            FRUIT_TWO,
            FRUIT_THREE,

            MOTH
        }

        private TYPE m_type = TYPE.FRUIT_ONE;

        private HexaTile m_tile = null;

        [SerializeField]
        private float m_mothStamina = 0.1f;

        [SerializeField]
        private float m_fruitStamina = 0.1f;

        private SpriteRenderer m_renderer = null;

        public void Init (HexaTile tile)
        {
            m_tile = tile;

            m_type = (Random.Range(0, 100) > 90) ? TYPE.MOTH : (TYPE)Random.Range(0, 3);

            m_renderer = GetComponent<SpriteRenderer>();

            m_renderer.sprite = Map.Instance.GetFoodSprite(m_type);

            transform.position = tile.data.POSITION;

            gameObject.SetActive(true);
        }

        public float Eat ()
        {
            m_tile.data.food = null;
            m_tile = null;

            FruitPool.Instance.Restore(this);
            
            return m_type != TYPE.MOTH ? m_fruitStamina : m_mothStamina;
        }
        
        public override void InitAction() { }
    }
}
