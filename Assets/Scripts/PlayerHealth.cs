using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private int maxHealth;
    private int currentHealth;
    private bool invulnerable = false;

    [SerializeField] private float IFramesDuration;
    [SerializeField] private float countFlashes;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private Sound playerHurt;
    [SerializeField] private Sound playerDie;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = HealthBar.fullHealthContainer * HealthBar.unlockedLifeContainers;
        currentHealth = maxHealth;
        healthBar.setHealthBar(currentHealth);
        invulnerable = false;
    }


    public void takeDamage() {
        if (invulnerable) return;
        currentHealth -= 1;
        healthBar.setHealthBar(currentHealth);
        if (currentHealth <= 0) {
            spriteRenderer.color = new Color(1, 0, 0, 1);
            AudioManager.instance.Play(playerDie);
            LevelSelector.ReturnToSelectLevel();
            GameManager.removeLevelMoney();
        }
        else {
            AudioManager.instance.Play(playerHurt);
            StartCoroutine(invulnerability());
        }
    }

    private IEnumerator invulnerability() {
        invulnerable = true;
        for (int i = 0; i < countFlashes; i++) {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (countFlashes * 2));
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (countFlashes * 2));
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        invulnerable = false;
    }

    public bool hasFullHealth() {
        maxHealth = HealthBar.fullHealthContainer * HealthBar.unlockedLifeContainers;
        return currentHealth >= maxHealth;
    }

    public void addHealth() {
        currentHealth++;
        healthBar.setHealthBar(currentHealth);
    }
}
