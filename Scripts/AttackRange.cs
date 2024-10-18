using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackRange : MonoBehaviour
{
    public int damage = 1;
    public bool hasNoTargetCountLimit = false;
    public int targetCount = 1;
    public string targetTag = "Boss";
    public float attackDuration = 0.5f;
    
    private List<GameObject> _attackedObjects = new List<GameObject>();
    
    private void OnEnable()
    {
        _attackedObjects.Clear();
        StartCoroutine(disableWithDelay());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(targetTag) && !_attackedObjects.Contains(col.gameObject))
        {
            col.gameObject.GetComponent<IDamaged>().Damaged(damage);
            _attackedObjects.Add(col.gameObject);
            targetCount--;
            if (targetCount <= 0 && !hasNoTargetCountLimit)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private IEnumerator disableWithDelay()
    {
        yield return new WaitForSeconds(attackDuration);
        gameObject.SetActive(false);
    } 
}
