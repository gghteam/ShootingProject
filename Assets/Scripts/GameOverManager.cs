using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText = null;
    private void Start()
    {
        
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
