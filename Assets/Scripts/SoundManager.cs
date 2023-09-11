using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        if (AudioListener.pause)
        {
            return;
        }
        _audioSource.PlayOneShot(buttonClickSound);
    }
}
