namespace GGJ
{
    using UnityEngine;

    public class HexaTile : MonoBehaviour
    {
        public enum TYPE
        {
            NORMAL = 0,

            ROCK,
            TREE,
            
            ANIMAL,
            FOOD
        }

        public Color color = Colors.White;

        public HexaTile[] aroundTiles = null;

        private int m_indexX = 0;
        public int indexX
        {
            get
            {
                return m_indexX;
            }
        }

        private int m_indexY = 0;
        public int indexY
        {
            get
            {
                return m_indexY;
            }
        }


        public void SetInfos (int x, int y, Vector2 position, TYPE type)
        {
            m_indexX = x;
            m_indexY = y;

            transform.localPosition = position;
        }
    }
}