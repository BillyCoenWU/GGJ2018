namespace GGJ
{
    using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_instance = null;
        public static T Instance
        {
            get
            {
                return s_instance;
            }

            protected set
            {
                if (s_instance != null)
                {
                    DestroyImmediate(value.gameObject);
                    return;
                }

                s_instance = value;
            }
        }
    }
}
