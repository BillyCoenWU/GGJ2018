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
        private SonarData m_sonarInfos = null;

        [SerializeField]
        private int m_mothFood = 1;

        [SerializeField]
        private int m_fruitFood = 1;

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

        public int Eat ()
        {
            m_tile.data.food = null;
            m_tile = null;

            FruitPool.Instance.Restore(this);
            
            return m_type != TYPE.MOTH ? m_fruitFood : m_mothFood;
        }

        public override void PlaySonar() { }
        public override void InitAction() { }
    }
}
