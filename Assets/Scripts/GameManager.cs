﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameObject enemyCroissant = null;
    [SerializeField]
    private GameObject enemyHotdog = null;
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text highScoreText = null;
    [SerializeField]
    private Text lifeText = null;

    public PoolManager poolManager { get; private set; }

    public int score = 0;
    public int highScore = 500;
    public int life = 2;

    public Vector2 minimumPosition;
    public Vector2 maximumPosition;

    private void OnEnable()
    {
        poolManager = FindObjectOfType<PoolManager>();
        UpdateLife();
        UpdateHighScore();
        StartCoroutine(SpawnCroissant());
    }

    public void UpdateScore()
    {
        scoreText.text = string.Format("SCORE\n{0}", score);
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
            UpdateHighScore();
        }
    }

    public void UpdateHighScore()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        highScoreText.text = string.Format("HIGHSCORE\n{0}", highScore);
    }

    public void UpdateLife()
    {
        lifeText.text = string.Format("x {0}", life);
    }

    private IEnumerator SpawnCroissant()
    {
        while (true)
        {
            float randomX = Random.Range(-6f, 6f);
            int count = 0;
            while(count < 5)
            {
                Instantiate(enemyCroissant, new Vector2(randomX, 15f), Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                count++;
            }
            float delay = Random.Range(2f, 5f);
            yield return new WaitForSeconds(delay);
        }
    }
}
