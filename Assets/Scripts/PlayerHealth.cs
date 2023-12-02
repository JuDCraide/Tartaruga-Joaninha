using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 3;
    private int currentHealth;

    void Start() {
        currentHealth = startingHealth;
    }

    void Update() {
        
    }

    public void takeDamage() {
        Debug.Log("Player Take Damage");
        currentHealth -= 1;
        if(currentHealth<=0) {
            Debug.Log("Player Die");
        }
    }
}
