using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public List<GameObject> targetPrefabs;

    private int _score;

    private int score
    {
        set
        {
            _score = Mathf.Max(value, 0);
        }
get
{
    return _score;
}
    }
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
        while (true && !gameOverText.IsActive())
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

IEnumerator waitSeconds()
{
    yield return new WaitForSeconds(5);
    SceneManager.LoadScene("Prototype 5");
}
private void SetGameOver()
{
    gameOverText.gameObject.SetActive(true);
    //Time.timeScale = 0;
    StartCoroutine(waitSeconds());
}
}


