namespace GGJ
{
    using UnityEngine;

    public class Food : GGJMonoBehaviour
    {
        [SerializeField]
        private float m_stamina = 0.1f;

        public float Eat ()
        {
            //Fazer voltar pra pool
            //Falar q o tile dele não tem mais fruta no data

            return m_stamina;
        }


        public override void InitAction() { }
    }
}
