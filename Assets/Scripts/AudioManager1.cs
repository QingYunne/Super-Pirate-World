using System.Collections;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    public static AudioManager1 Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [System.Serializable]
    public class Sound
    {
        public SoundType type;
        public AudioClip clip;
    }

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    private void OnEnable()
    {
        EventManager.Subscribe("PlaySFX", OnPlaySFX);
        EventManager.Subscribe("PlayMusic", OnPlayMusic);
        EventManager.Subscribe("StopMusic", OnStopMusic);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("PlaySFX", OnPlaySFX);
        EventManager.Unsubscribe("PlayMusic", OnPlayMusic);
        EventManager.Unsubscribe("StopMusic", OnStopMusic);
    }

    private AudioClip GetClip(SoundType type)
    {
        foreach (var s in sounds)
            if (s.type == type) return s.clip;
        return null;
    }

    private void OnPlayMusic(object data)
    {
        if (data is SoundEventData sound)
        {
            AudioClip clip = GetClip(sound.Type);
            if (clip == null) return;

            musicSource.clip = clip;
            musicSource.volume = sound.Volume;
            musicSource.loop = sound.Loop;
            musicSource.time = sound.StartTime;
            musicSource.Play();
        }
    }

    private void OnStopMusic(object data)
    {
        musicSource.Stop();
    }

    private void OnPlaySFX(object data)
    {
        if (data is SoundEventData sound)
        {
            AudioClip clip = GetClip(sound.Type);
            if (clip == null) return;

            if (sound.DuckMusic)
                StartCoroutine(DuckMusic(sound.DuckVolume, sound.DuckDuration));

            sfxSource.PlayOneShot(clip, sound.Volume);
        }
    }

    private IEnumerator DuckMusic(float targetVolume, float duration)
    {
        float originalVolume = musicSource.volume;
        musicSource.volume = targetVolume;

        yield return new WaitForSeconds(duration);

        musicSource.volume = originalVolume;
    }
}
