using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private ParticleSystem doorParticles;
    [SerializeField] private AudioClip doorOpeningSound;

    private EnemiesManager _enemiesManager;
    private static readonly int Open = Animator.StringToHash("Open");

    private bool _doorOpened = false;

    private void Awake()
    {
        _enemiesManager = FindObjectOfType<EnemiesManager>();
        
        _enemiesManager.OnAllEnemiesDead += OnAllEnemiesDead;
    }

    private void OnAllEnemiesDead(object sender, EventArgs e)
    {
        if (_doorOpened)
        {
            return;
        }
        OpenDoor();
    }

    private void OpenDoor()
    {
        doorAnimator.SetTrigger(Open);
        doorParticles.Play();
        AudioSource.PlayClipAtPoint(doorOpeningSound, transform.position, .5f);
        _doorOpened = true;
    }

    private void OnDisable()
    {
        _enemiesManager.OnAllEnemiesDead -= OnAllEnemiesDead;
    }
}
