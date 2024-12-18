using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static int currentLevel = 1;
    private static int levelMoney = 0;

    public GameObject playerRef { get; private set; }
    public TMPro.TextMeshProUGUI moneyText = null;
    public static HashSet<int> livesCollected = new HashSet<int>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    void Start() {

    }

    void Update() {
        if (moneyText != null) {
            showMoney();
        }
    }

    public void setPlayer(GameObject newPlayer) {
        playerRef = newPlayer;
    }

    public GameObject getPlayer() {
        return playerRef;
    }

    public void addMoney(int value) {
        levelMoney += value;
    }

    public static void removeLevelMoney() {
        levelMoney = 0;
    }

    public void showMoney() {
        moneyText.text = (levelMoney + Money.getMoney()).ToString();
    }

    public static void EndLevel(int currentLevel) {
        Money.addMoney(levelMoney);
        LevelSelector.UnlockNextLevel(currentLevel);
        LevelSelector.ReturnToSelectLevel();
        levelMoney = 0;
    }

    public static void unselectHat() {
        Hats.unselectHat();
    }

    public static void collectLife() {
        livesCollected.Add(currentLevel);
    }

    public static bool lifeCollected() {
        if (livesCollected.Contains(currentLevel)) {
            return true;
        }
        return false;
    }

}
