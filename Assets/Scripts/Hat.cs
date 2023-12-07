using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Hat {
    public int id;
    public Sprite sprite;
    public int price;

    public bool bought = false;
    public bool selected = false;

    public void BuyHat() {
        bought = true;
        Money.spendMoney(price);
        Hats.SetHat(id, this);
    }

    public void SelectHat() {
        selected = true;
        Hats.selectHat(this);
        Hats.SetHat(id, this);
    }

    public void UnselectHat() {
        selected = false;
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
