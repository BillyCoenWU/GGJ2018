namespace GGJ
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public enum LANGUAGE
    {
        NONE = -1,

        ENGLISH = 0,
        PORTUGUESE,

        TOTAL
    }

    public enum SCENE
    {
        NONE = -1,

        MAIN = 0,

        TOTAL
    }

    public interface IUpdate
    {
        void CustomUpdate();
    }

    public interface IFixedUpdate
    {
        void CustomFixedUpdate();
    }

    public interface ILateUpdate
    {
        void CustomLateUpdate();
    }

    public class Game : MonoBehaviour
    {
        public class Save
        {
            public static void SetKey<T>(string key, T value)
            {
                if (typeof(T) == typeof(int))
                {
                    PlayerPrefs.SetInt(key, (int)(object)value);
                }
                else if (typeof(T) == typeof(string))
                {
                    PlayerPrefs.SetString(key, (string)(object)value);
                }
                else if (typeof(T) == typeof(float))
                {
                    PlayerPrefs.SetFloat(key, (float)(object)value);
                }
            }

            public static T GetKey<T>(string key, T defaultValue)
            {
                if (typeof(T) == typeof(int))
                {
                    return (T)(object)PlayerPrefs.GetInt(key, (int)(object)defaultValue);
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)(object)PlayerPrefs.GetString(key, (string)(object)defaultValue);
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)PlayerPrefs.GetFloat(key, (float)(object)defaultValue);
                }

                return defaultValue;
            }
        }
        
        private static LANGUAGE m_language = LANGUAGE.ENGLISH;
        public static LANGUAGE language
        {
            get
            {
                return m_language;
            }
        }

        private static bool s_pause = false;
        public static bool IsPaused
        {
            get
            {
                return s_pause;
            }
        }

        public delegate void UpdateEvents();

        public static event UpdateEvents update;
        public static event UpdateEvents lateUpdate;
        public static event UpdateEvents fixedUpdate;

        private void Awake ()
        {
            DontDestroyOnLoad(gameObject);

            SetLanguage((LANGUAGE)Save.GetKey<int>("Language", 0));
        }

        private void Update ()
        {
            update?.Invoke();
        }

        private void FixedUpdate ()
        {
            fixedUpdate?.Invoke();
        }
        
        private void LateUpdate ()
        {
            lateUpdate?.Invoke();
        }

        private void activeSceneChanged (Scene oldScene, Scene NewScene)
        {
            update = null;
            lateUpdate = null;
            fixedUpdate = null;
        }

        public static void Pause ()
        {
            s_pause = true;
            Time.timeScale = 0.0f;
        }

        public static void Resume ()
        {
            s_pause = false;
            Time.timeScale = 1.0f;
        }

        public static void SetLanguage (LANGUAGE language)
        {
            m_language = language;

            Save.SetKey<int>("Language", (int)m_language);
        }

        public static void LoadScene (SCENE scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadSceneAsync((int)scene, mode);
        }
    }
}
