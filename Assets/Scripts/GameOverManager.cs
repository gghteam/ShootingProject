﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
