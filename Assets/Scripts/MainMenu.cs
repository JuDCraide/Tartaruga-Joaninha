using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
    }

    public void ChangeScene(string sceneName) {
          SceneManager.LoadScene(sceneName);
    }

    public void restartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
