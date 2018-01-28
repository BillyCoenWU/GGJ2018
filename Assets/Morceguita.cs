namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Morceguita : GGJMonoBehaviour
    {
        [SerializeField]
        private SonarData m_sonarInfos = null;

        private HexaTile m_tile = null;

        public void Init(HexaTile tile)
        {
            m_tile = tile;
        }

        public override void PlaySonar() { SonarPool.Instance.Load().Set(m_sonarInfos.sprite, transform.position, m_sonarInfos.maxScale, m_sonarInfos.maxCount,false); }
        public override void InitAction() { }
    }
}
