namespace GGJ
{
    using UnityEngine;

    public class Bat : GGJMonoBehaviour
    {
        private HexaTile m_tile = null;

        public void SetInitialTile (HexaTile tile)
        {
            m_tile = tile;

            transform.localPosition = m_tile.data.POSITION;
        }

        public override void InitAction()
        {

        }
    }
}
