using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hat : MonoBehaviour {
    [SerializeField] public int id;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;

    public bool bought = false;
    private bool selected = false;

    [SerializeField] private TMPro.TextMeshProUGUI hatText;
    [SerializeField] private Image hatImage;

    public void Start() {
        hatImage.GetComponent<Image>().sprite = sprite;
        Hat savedHat = Hats.GetHat(id);
        if (savedHat == null) {
            Hats.SetHat(id, this);
        }
        else {
            bought = savedHat.bought;
            selected = savedHat.selected;
        }
        hatText.text = SetText();
    }

    public void OnClick() {
        if (!bought) {
            BuyHat();
        }
        else if (!selected) {
            SelectHat();
        }
        Hats.GetHat(id);
    }

    public void BuyHat() {
        bought = true;
        Money.spendMoney(price);
        hatText.text = SetText();
        Hats.SetHat(id, this);
    }

    public void SelectHat() {
        selected = true;
        Hats.selectHat(this);
        hatText.text = SetText();
        Hats.SetHat(id, this);
    }

    public void UnselectHat() {
        selected = false;
        hatText.text = SetText();
        Hats.SetHat(id, this);
    }

    public string SetText() {
        if (selected) {
            return "Selecionado";
        }
        if (bought) {
            return "Selecionar";
        }
        return price.ToString();

    }
}
