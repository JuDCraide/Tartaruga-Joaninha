using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 3;
    private int currentHealth;
    private bool invulnerable = false;

    [SerializeField] private float IFramesDuration;
    [SerializeField] private float countFlashes;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int[] targetNumLayer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }

    void Start() {
        // ignoreAllLayersCollision(false);
        invulnerable = false;
    }

    public void takeDamage() {
        if(invulnerable) return;
        currentHealth -= 1;
        if(currentHealth<=0) {
            Debug.Log("Player Die");
            spriteRenderer.color = new Color(1, 0, 0, 1);
        } else {
            StartCoroutine(invulnerability());
        }
    }

    // private void ignoreAllLayersCollision(bool ignore){
    //     foreach (int layer in targetNumLayer) {
    //         Physics2D.IgnoreLayerCollision(gameObject.layer, layer, ignore);
    //     }
    // }

    private IEnumerator invulnerability() {
        Debug.Log("INVULERABILITY");
        // ignoreAllLayersCollision(true);
        invulnerable = true;
        for (int i = 0; i < countFlashes; i++) {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (countFlashes * 2));
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (countFlashes * 2));
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        // ignoreAllLayersCollision(false);
        invulnerable = false;
    }
}
