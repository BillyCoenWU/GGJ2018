namespace GGJ
{
    using UnityEngine;

    public class Obstacle : GGJMonoBehaviour
    {
        [SerializeField]
        private SonarData m_sonarInfos = null;

        private HexaTile.TYPE m_type = HexaTile.TYPE.TREE;

        private HexaTile m_tile = null;

        private SpriteRenderer m_renderer = null;

        public void SetType (HexaTile tile)
        {
            m_tile = tile;
            m_type = (Random.Range(0, 100) > 50) ? HexaTile.TYPE.TREE : HexaTile.TYPE.ROCK;

            transform.localPosition = m_tile.data.POSITION;
            
            m_renderer = GetComponent<SpriteRenderer>();
            m_renderer.sprite = Map.Instance.GetSprite(m_type);

            gameObject.SetActive(true);
        }

        public override void PlaySonar() { SonarPool.Instance.Load().Set(m_sonarInfos.sprite, transform.position, m_sonarInfos.maxScale, m_sonarInfos.maxCount,false); }
        public override void InitAction() {}
    }
}
