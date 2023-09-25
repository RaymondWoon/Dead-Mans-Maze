using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    //[SerializeField]
    public static int currentHp = 100;
    public static int maxHp = 100;

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
