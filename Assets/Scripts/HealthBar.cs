using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public static int maxLifeContainers = 3;
    public static int unlockedLifeContainers = 1;
    public static int fullHealthContainer = 3;

    [SerializeField] private GameObject[] lives;

    void Start() {

    }

    void Update() {

    }

    void setLifeImage(GameObject live, string name) {
        for (int i = 0; i < live.transform.childCount; i++) {
            live.transform.GetChild(i).gameObject.SetActive(false);
        }

        live.transform.Find(name).gameObject.SetActive(true);
    }

    public void setHealthBar(int currentHealth) {
        for (int i = 0; i < maxLifeContainers; i++) {
            if (i >= unlockedLifeContainers) {
                setLifeImage(lives[i], "Broken");
            }
            else if (currentHealth >= 3) {
                setLifeImage(lives[i], "3");
                currentHealth -= 3;
            }
            else if (currentHealth > 0) {
                setLifeImage(lives[i], currentHealth.ToString());
                currentHealth = 0;
            }
            else {
                setLifeImage(lives[i], "Empty");
            }
        }
    }
}
