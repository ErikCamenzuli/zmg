using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WaveSpawner : MonoBehaviour
{
    //Reference of enemies/spawn points array
    [SerializeField]private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    //Enemy Variable
    [SerializeField] private int baseEnemies = 0;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyWaveScale = 0.75f;
    private int currentWave = 1;
    private int enemiesAlive;
    private int enemiesLeft;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    //Wave Variable
    private float timeSinceLastSpawn;
    private bool isSpawning = false;
    private int randomSpawns;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeft > 0)
        {
            SpawnEnemy();
            enemiesLeft--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeft == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeft = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyWaveScale));
    }

    private void SpawnEnemy()
    {
        GameObject spawnPrefab = enemyPrefabs[0];
        randomSpawns = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(spawnPrefab, spawnPoints[randomSpawns].position, Quaternion.identity);
    }
}
