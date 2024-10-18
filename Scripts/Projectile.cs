using System.Collections;
using Boss;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed = 5f;
    public bool homing = false;
    public float lifeTime = 5f;
        
        
    private GameObject _target;
    private Vector2 _direction;
    void Start()
    {
        _target = GameObject.Find("Boss");
        LookAtTarget();
        _direction = (_target.transform.position - transform.position).normalized;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        // 호밍 여부
        if (homing)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, speed * Time.deltaTime);
            LookAtTarget();
        }
        // 호밍 아닐 시 direction으로 이동
        else
        {
            transform.position += (Vector3)(_direction * speed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
    
    public void LookAtTarget()
    {
        Vector3 dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag($"Boss"))
        {
            other.gameObject.GetComponent<BossController>().Damaged(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float attackDamage)
    {
        damage = attackDamage;
    }
    
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}