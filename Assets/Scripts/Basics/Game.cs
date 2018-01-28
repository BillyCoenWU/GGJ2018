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
        INGAME,

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
        }

        private void Update ()
        {
            if (update != null)
            {
                update.Invoke();
            }
        }

        private void FixedUpdate ()
        {
            if (fixedUpdate != null)
            {
                fixedUpdate.Invoke();
            }
        }
        
        private void LateUpdate ()
        {
            if (lateUpdate != null)
            {
                lateUpdate.Invoke();
            }
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
        
        public static void LoadScene (SCENE scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadSceneAsync((int)scene, mode);
        }
    }
}
