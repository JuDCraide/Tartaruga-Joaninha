using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    private static int unlockedLevel = 1;
    [SerializeField] private int level;
    [SerializeField] private bool historyLevel = false;

    public TMPro.TextMeshProUGUI levelName;

    void Awake() {
        if (level > unlockedLevel) {
            gameObject.GetComponent<Button>().interactable = false;
        }
        if (!historyLevel) {
            levelName.text = "Nível \n" + level.ToString();
        }
    }

    public void LoadLevel(string sceneName) {
        GameManager.currentLevel = level;
        SceneManager.LoadScene(sceneName);
    }

    public static void UnlockNextLevel(int currentLevel) {
        if (currentLevel == unlockedLevel) {
            unlockedLevel++;
        }
        else if (currentLevel > unlockedLevel) {
            unlockedLevel = currentLevel;
        }
    }
    public static void ReturnToSelectLevel() {
        SceneManager.LoadScene("SelectLevel");
    }
}
