using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText = null;
    private void Start()
    {
        highScoreText.text = string.Format("HIGHSCORE\n{0}", PlayerPrefs.GetInt("HIGHSCORE", 100000));
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
