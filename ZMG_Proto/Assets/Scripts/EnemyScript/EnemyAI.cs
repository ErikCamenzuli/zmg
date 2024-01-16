using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int points;
    public NavMeshAgent enemyAgent;
    GameObject target;

    public int health = 50;
    public PlayerHealth playerHealth;
    public int attackDamage;
    public int enemyPoints;
    private int enemyScore;
    public int xpAmount;
    public EnemyKillCount killCounter;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        killCounter = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyKillCount>();
    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
    }

    private void Die()
    {
        Destroy(this);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            enemyScore = enemyPoints * killCounter.kills;
            WaveSpawner.onEnemyDestroy.Invoke();
            ScoreManager.scoreInstance.AddPoints(enemyScore);
            killCounter.AddKill();
            Die();
        }
    }

    private void GoToTarget()
    {
        enemyAgent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().UpdatePlayerHealth(-attackDamage);
        }
    }

}
