using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharacterStat Strenght;
}

public class Item
{
    public void Equip(Character c)
    {
     
        c.Strenght.AddMod(new StatMod(10, StatModType.Flat, this));
        c.Strenght.AddMod(new StatMod(0.1f, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.Strenght.RemoveAllModFromSource(this);
    }
  
}
