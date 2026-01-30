using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform spawnTransform;


    private void Reset()
    {
        spawnTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Wave
{
    [Header("Tipos de enemigos en esta oleada")]
    public GameObject[] enemyPrefabs;

    [Header("Cantidad total de enemigos")]
    public int count = 5;

    [Header("Tiempo entre spawns")]
    public float spawnRate = 1f;
}

