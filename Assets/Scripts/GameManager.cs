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

    public int score = 0;
    public int highScore = 500;
    public int life = 2;

    public Vector2 minimumPosition;
    public Vector2 maximumPosition;

    private void Start()
    {
        UpdateLife();
        UpdateHighScore();
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
}
