using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody body;
    public float lifeTime;
    public int damage = 25;

    public void Activate(Vector3 position, Vector3 velocity)
    {
        transform.position = position;
        body.velocity = velocity;

        gameObject.SetActive(true);
        StartCoroutine("Decay");
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(lifeTime);
        Deactivate();
    }
    
    public void Deactivate()
    {
        BulletPool.main.AddToPool(this);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.TryGetComponent(out EnemyAI enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
            Debug.Log(EnemyAI.health);
        }
        Deactivate();
    }

}
