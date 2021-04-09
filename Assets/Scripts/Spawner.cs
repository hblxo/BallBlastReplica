using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    public GameObject[] bullets;
    public Transform[] spawnPoints;

    public float minDelay = 2f;
    public float maxDelay = 3f;

    public GameObject coinPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnBullets()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            
            yield return new WaitForSeconds(delay);
            
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
       
            if (GameManager.instance.coinsCount < GameManager.instance.maxCoins)
                Instantiate(bullets[Random.Range(0, bullets.Length)], spawnPoint);
        }
    }
}
