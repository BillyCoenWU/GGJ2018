namespace RGSMS
{
    namespace Sound
    {
        using UnityEngine;
        using UnityEngine.Events;

        [RequireComponent(typeof(AudioSource))]
        public class GameAudioObject : MonoBehaviour
        {
            [SerializeField]
            private bool m_playOnStart = false;
            [SerializeField]
            private bool m_dontDestroyOnLoad = false;

            [Space]

            [SerializeField]
            private SoundData m_data = null;

            [Space]

            [SerializeField]
            private UnityEvent m_onAudioEnd;
            
            private AudioSource m_source = null;

            private const string METHOD = "InvokeEvents";
            private const string METHOD_TWO = "PlaySequentialAndRandomSounds";

            private void Awake()
            {
                if(m_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
                
                m_source = GetComponent<AudioSource>();
                LoadData();

                if(m_playOnStart)
                {
                    Play();
                }
            }
            
            private void LoadData()
            {
                if (m_source.clip != null)
                {
                    Stop();
                }

                m_source.clip = m_data.GetClip();
                m_source.volume = m_data.volume;
                m_source.pitch = m_data.pitch;
                m_data.audioSource = m_source;

            }

            private void InvokeEvents()
            {
                m_onAudioEnd.Invoke();
            }

            public void SetNewData(SoundData newData)
            {
                m_data = newData;
                LoadData();
            }

            public void Play()
            {
                Stop();
                m_source.clip = m_data.GetClip();
                m_source.Play();

                if (m_onAudioEnd.GetPersistentEventCount() > 0)
                {
                    Invoke(METHOD, m_source.clip.length);
                }
            }

            public void Play(bool loop)
            {
                m_source.Stop();
                m_source.loop = loop;
                m_source.clip = m_data.GetClip();
                m_source.Play();

                if (m_onAudioEnd.GetPersistentEventCount() > 0)
                {
                    InvokeRepeating(METHOD, m_source.clip.length, m_source.clip.length);
                }
            }

            public void PlaySequentialAndRandomSounds ()
            {
                m_source.Stop();
                m_source.clip = m_data.GetClip();
                m_source.Play();

                Invoke(METHOD_TWO, m_source.clip.length);
            }

            public void Play(float delay)
            {
                Stop();
                m_source.clip = m_data.GetClip();
                m_source.PlayDelayed(delay);

                if (m_onAudioEnd.GetPersistentEventCount() > 0)
                {
                    Invoke(METHOD, (m_source.clip.length + delay));
                }
            }

            public void Play(float delay, bool loop)
            {
                Stop();
                m_source.loop = loop;
                m_source.clip = m_data.GetClip();
                m_source.PlayDelayed(delay);

                if (m_onAudioEnd.GetPersistentEventCount() > 0)
                {
                    InvokeRepeating(METHOD, (m_source.clip.length+delay), m_source.clip.length);
                }
            }

            public void Stop()
            {
                CancelInvoke();
                m_source.Stop();
            }

            public void Pause()
            {
                m_source.Pause();
            }

            public void UnPause()
            {
                m_source.UnPause();
            }

            public void Volume(float volume)
            {
                m_source.volume = volume;
            }
        }
    }
}
