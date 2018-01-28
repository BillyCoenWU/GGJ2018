namespace GGJ
{
    using UnityEngine;
    using System.Collections;

    public class Morceguita : GGJMonoBehaviour
    {
        [SerializeField]
        private SonarData m_sonarInfos = null;

        private HexaTile m_tile = null;

        public void Init (HexaTile tile)
        {
            m_tile = tile;
        }

        public override void PlaySonar() {}
        public override void InitAction() {}
    }
}
