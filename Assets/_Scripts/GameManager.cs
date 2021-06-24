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


private void SetGameOver()
{
    gameState = GameState.gameOver;
    gameOverText.gameObject.SetActive(true);
    restartButton.gameObject.SetActive(true);
    
    //Time.timeScale = 0;
    //StartCoroutine(waitSeconds());
}

/// <summary>
/// Inicia la partida cambiando el valor del estado del juego
/// </summary>
/// <param name="difficulty">grado de dificultad del juego</param>
public void startGame(int difficulty)
{
    spawnRate /=  difficulty;
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


