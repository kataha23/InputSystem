using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("적 설정")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPoints;
    [Header("소환 설정")]
    [SerializeField] private float spawnRateMin = 0.5f;
    [SerializeField] private float spawnRateMax = 5f;

    private float timer;
    private float SpawnRate;    
    private void Start()
    {
        SpawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > SpawnRate)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = transform.position;

            SpawnRate = Random.Range(spawnRateMin, spawnRateMax);
            timer = 0f;
        }
    }
}
