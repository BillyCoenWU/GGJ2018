namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class TilesPool : Singleton<TilesPool>
    {
        [SerializeField]
        private int m_count = 880;

        [SerializeField]
        private Object m_reference = null;

        private Queue<HexaTile> m_tiles = null;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitPool();
        }
        
        private void InitPool ()
        {
            m_tiles = new Queue<HexaTile>();

            for(int  i= 0; i < m_count; i++)
            {
                Restore(GetTile());
            }
        }

        public HexaTile Load ()
        {
            return m_tiles.Count > 0 ? m_tiles.Dequeue() : GetTile();
        }

        public void Restore (HexaTile tile)
        {
            m_tiles.Enqueue(tile);

            tile.gameObject.SetActive(false);
            tile.transform.SetParent(transform);
        }

        private HexaTile GetTile ()
        {
            return CreateByReference().GetComponent<HexaTile>();
        }

        private GameObject CreateByReference ()
        {
            return (GameObject)Instantiate(m_reference, Constantes.VECTOR_THREE_ZERO, Constantes.IDENTITY);
        }
    }
}
