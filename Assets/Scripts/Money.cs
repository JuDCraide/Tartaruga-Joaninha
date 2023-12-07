using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money {
    private static int money;

    public static void addMoney(int value) {
        money += value;
    }

    public static void spendMoney(int value) {
        money -= value;
    }

    public static int getMoney() {
        return money;
    }
}
