using UnityEngine;

public class SoudStarMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource vfxAudioSource;

    [SerializeField] private AudioClip musicClip;
    public AudioClip clickButtonClip;

    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = 0.5f;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip, float volumn = 1)
    {
        vfxAudioSource.volume = volumn;
        musicAudioSource.volume = 0.3f;
        vfxAudioSource.PlayOneShot(sfxClip);
    }
}
