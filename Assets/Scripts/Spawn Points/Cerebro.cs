using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Puntos de spawn")]
    public SpawnPoint[] spawnPoints;

    [Header("Oleadas")]
    public Wave[] waves;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Update()
    {
        // Si no hay oleada en curso y no quedan enemigos vivos, empieza la siguiente
        if (!waveInProgress && enemiesAlive == 0)
        {
            StartCoroutine(StartWave());
        }
    }

    IEnumerator StartWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("Todas las oleadas completadas.");
            yield break;
        }

        waveInProgress = true;
        Wave wave = waves[currentWaveIndex];

        Debug.Log("Iniciando oleada " + (currentWaveIndex + 1));

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        waveInProgress = false;
        currentWaveIndex++;
    }

    void SpawnEnemy(Wave wave)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay spawn points asignados.");
            return;
        }

        // Punto aleatorio
        Transform spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].spawnTransform;

        // Enemigo aleatorio de la oleada
        GameObject prefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawnPos.position, spawnPos.rotation);

        enemiesAlive++;


    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
