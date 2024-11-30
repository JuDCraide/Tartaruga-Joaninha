using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelect : MonoBehaviour {
    [SerializeField] public Hat hat;

    [SerializeField] private TMPro.TextMeshProUGUI hatText;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private Image hatImage;

    private Button button;

    public void Start() {
        hatImage.GetComponent<Image>().sprite = hat.sprite;
        Hat savedHat = Hats.GetHat(hat.id);
        if (savedHat == null) {
            Hats.SetHat(hat.id, hat);
        }
        else {
            hat.bought = savedHat.bought;
            hat.selected = savedHat.selected;
        }
        hatText.text = hat.SetText();

        button = gameObject.GetComponent<Button>();
    }

    public void Update() {
        hatText.text = hat.SetText();
        if (!hat.bought && Money.getMoney() < hat.price) {
            button.interactable = false;
        }
        if (hat.bought) {
            coinImage.SetActive(false);
        }
    }

    public void OnClick() {
        if (!hat.bought) {
            hat.BuyHat();
        }
        else if (!hat.selected) {
            hat.SelectHat();
        }
    }

}
