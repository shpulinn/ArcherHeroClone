using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footStepSounds;
    [Range(0, 1)] [SerializeField] private float volume = .1f;

    public void PlayLeftFootStepSound()
    {
        AudioSource.PlayClipAtPoint(GetRandomSound(), transform.position, volume);
    }
    
    public void PlayRightFootStepSound()
    {
        AudioSource.PlayClipAtPoint(GetRandomSound(), transform.position, volume);
    }

    private AudioClip GetRandomSound()
    {
        return footStepSounds[Random.Range(0, footStepSounds.Count)];
    }
}
