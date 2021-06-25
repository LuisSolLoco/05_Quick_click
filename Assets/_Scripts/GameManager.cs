using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public List<GameObject> targetPrefabs;
    public GameObject titleScreen;
    private int _score;
    private int numberOfLives = 3;
    public List<GameObject> lives;

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

   


    // Update is called once per frame
    IEnumerator SpawnTarget()
    {
        
        while (gameState==GameState.inGame)
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

public void showMaxScore()
{
    int maxScore = PlayerPrefs.GetInt("MAX_SCORE");
    scoreText.text = "Max score: \n" + maxScore;
}

private void SetMaxScore()
{
    int maxScore = PlayerPrefs.GetInt("MAX_SCORE");

    if (score> maxScore)
    {
        PlayerPrefs.SetInt("MAX_SCORE", score);
        //TODO: si hay nueva puntuacion maxima lanzar cohetes
    }
}
private void SetGameOver()
{
    //decremento el numero de vidas en uno
    
    numberOfLives--;

    Image heartImage = lives[numberOfLives].GetComponent<Image>();
    var tempColor = heartImage.color;
    tempColor.a = 0.30f;
    heartImage.color = tempColor;
    if (numberOfLives <= 0)
    {
        SetMaxScore();


        gameState = GameState.gameOver;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    
    //Time.timeScale = 0;
    //StartCoroutine(waitSeconds());
}

private void Start()
{
    
    showMaxScore(); 
}

/// <summary>
/// Inicia la partida cambiando el valor del estado del juego
/// </summary>
/// <param name="difficulty">grado de dificultad del juego</param>
public void startGame(int difficulty,int livesCount)
{
    spawnRate /=  difficulty;
    numberOfLives = livesCount;

    
    for (int i = 0; i < numberOfLives; i++)
    {
        lives[i].SetActive(true);
    }
    titleScreen.SetActive(false);
    gameState = GameState.inGame;
    StartCoroutine(SpawnTarget());
    score = 0;
    UpdateScore(0);
    
}
public void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}


