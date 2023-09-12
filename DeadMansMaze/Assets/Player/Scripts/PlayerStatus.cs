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

    public int AddHp(int hp)
    {
        if (currentHp + hp > maxHp)
        { 
            currentHp = maxHp;
            return currentHp;
        }
        currentHp = hp;
        return currentHp;
    }
}
