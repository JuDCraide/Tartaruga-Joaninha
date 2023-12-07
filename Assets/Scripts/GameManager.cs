using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject playerRef { get; private set; }
    public TMPro.TextMeshProUGUI moneyText;

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

    }

    public void setPlayer(GameObject newPlayer) {
        playerRef = newPlayer;
    }

    public GameObject getPlayer() {
        return playerRef;
    }

    public void addMoney(int value) {
        Money.addMoney(value);
        int money = Money.getMoney();
        moneyText.text = money.ToString("D7");
    }

    public void unselectHat() {
        Hats.unselectHat();
    }
}
