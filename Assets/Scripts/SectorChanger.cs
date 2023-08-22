using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SectorChanger : MonoBehaviour
{
    public UnityEvent OnPlayerMovedToSecondSector;

    [SerializeField] private GameObject sectorToEnable;
    [SerializeField] private GameObject sectorToDisable;
    [SerializeField] private Transform playerEnterPosition;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private BoxCollider newCameraConfiner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sectorToEnable.SetActive(true);
            other.GetComponent<NavMeshAgent>().Warp(playerEnterPosition.position);
            camera.GetComponent<CinemachineConfiner>().m_BoundingVolume = newCameraConfiner;
            sectorToDisable.SetActive(false);
            OnPlayerMovedToSecondSector?.Invoke();
            other.GetComponent<PlayerMotor>().ChangeState(other.GetComponent<IdleState>());
        }
    }
}
