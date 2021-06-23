using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour

{
    
public float spawnRate = 1.0f;


    public List<GameObject> targetPrefabs;
    // Start is called before the first frame update

   

    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targetPrefabs.Count );
            Instantiate(targetPrefabs[index]);
        }
    }
}


