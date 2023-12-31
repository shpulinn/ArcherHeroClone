using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public EventHandler OnAllEnemiesDead;
    
    [SerializeField] private LayerMask enemiesLayerMask;

    private List<Collider> enemiesAlive = new List<Collider>();
    void Start()
    {
        //Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, enemiesLayerMask);
        List<Collider> colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity,
            enemiesLayerMask).ToList();
        enemiesAlive = colliders.ToList();
        
        EnemyHealth.OnAnyEnemyDieEvent += OnAnyEnemyDieEvent;
    }

    private void OnAnyEnemyDieEvent(object sender, EnemyHealth eh)
    {
        enemiesAlive.Remove(eh.GetComponent<Collider>());
        //RefreshEnemiesList();
        if (enemiesAlive.Count == 0)
        {
            OnAllEnemiesDead?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RefreshEnemiesList()
    {
        //Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, enemiesLayerMask);
        //List<Collider> colliders = Physics.OverlapSphere(transform.position, 30f, enemiesLayerMask).ToList();
        List<Collider> colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity,
            enemiesLayerMask).ToList();
        // for (int i = 0; i < colliders.Count; i++)
        // {
        //     if (colliders[i] == null)
        //     {
        //         colliders.Remove(colliders[i]);
        //     }
        // }

        //colliders = colliders.Where(gameObject => gameObject != null).ToList();
        //colliders.RemoveAll(x => !x);

        enemiesAlive = colliders;//.ToList();
        Debug.Log("refreshed. enemies list count: " + enemiesAlive.Count);
    }

    public bool IsAnyEnemyAlive()
    {
        return enemiesAlive.Count > 0;
    }

    public Collider[] GetEnemiesColliders()
    {
        return enemiesAlive.ToArray();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 30f);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
