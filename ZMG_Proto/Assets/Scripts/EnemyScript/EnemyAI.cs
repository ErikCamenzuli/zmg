using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int points;
    public NavMeshAgent enemyAgent;
    Transform target;

    public static int health = 50;
    public PlayerHealth playerHealth;
    public int attackDamage = 10;
    private int enemyPoints = 10;
    private int enemyScore;
    public int xpAmount;
    public EnemyKillCount killCounter;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        killCounter = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyKillCount>();
    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            enemyScore = enemyPoints * killCounter.kills;
            WaveSpawner.onEnemyDestroy.Invoke();
            ScoreManager.scoreInstance.AddPoint(points);
            ScoreManager.scoreInstance.AddPoint(enemyScore);
            killCounter.AddKill();
            Die();
        }
    }

    private void GoToTarget()
    {
        enemyAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth healthComponent))
            {
                healthComponent.UpdatePlayerHealth(attackDamage);
                Debug.Log("Player Hit for: " + attackDamage);
            }
        }
    }

}
