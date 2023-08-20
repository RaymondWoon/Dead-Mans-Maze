using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    [SerializeField]
    private int currentHp = 100;
    private int maxHp = 100;
    [SerializeField]
    public Inventory inventory = new Inventory();

    public int GetCurrentHp()
    {
        return currentHp;
    }

    public void SetCurrentHp(int hp)
    {
        if (hp > maxHp)
        { 
            currentHp = maxHp;
            return;
        }
        currentHp = hp;
    }
}
