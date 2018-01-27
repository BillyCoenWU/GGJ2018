namespace GGJ
{
    using UnityEngine;
    
    public class Bird : GGJMonoBehaviour
    {
        public enum TYPE
        {
            HAWK = 0,
            OWL
        }

        private TYPE m_type = TYPE.HAWK;

        [SerializeField]
        private int m_maxActionsPerTurn = 1;
        
        private SpriteRenderer m_renderer = null;
        
        public void Init ()
        {
            m_type = (Random.Range(0, 100) > 50) ? TYPE.HAWK : TYPE.OWL;

            m_renderer = GetComponent<SpriteRenderer>();

            m_renderer.sprite = Map.Instance.GetAnimalSprite(m_type);
        }

        public override void InitAction()
        {


        }
    }
}
