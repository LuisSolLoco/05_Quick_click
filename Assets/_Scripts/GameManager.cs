using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour

{
    
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public List<GameObject> targetPrefabs;

    private int score;
    // Start is called before the first frame update

   

    void Start()
    {
        StartCoroutine(SpawnTarget());
        score = 0;
       UpdateScore(0);
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
/// <summary>
/// Actualiza la puntuacion
/// </summary>
/// <param name="ScoreToAdd">numero de puntos a añadir</param>
    private void UpdateScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = "Score: " + score;
    }
}


