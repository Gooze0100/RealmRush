using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    // you can create pool in start, but it is good to create them in Awake
    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            // transform is the parent so it is created in that empty object
            // It is problem that if we give 0 then infinite loop and if negative also bad
            //so it can crash our game
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}

/*
So what is object pool?
it is used to manage performance issues, because when game is getting bigger and spawning enemies and so on it will have some performance issues
we will create our objects of the start of the level and it will be deactivated that player cant see them
and when we need then we can get by one from that pool and activate it 
they just get deactivated and get back at the Object pool and is being reused and not destroyed

Object pool is created of fixed size so we cannot add additional enemies after we create it
it could be wave shift system

*/