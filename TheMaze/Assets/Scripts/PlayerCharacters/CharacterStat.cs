using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


//mod = modifier


[Serializable]
public class CharacterStat
{

    public float BaseValue;

    public virtual float Value {
        get
        {
            if (IsDirty  || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                IsDirty = false;
            }

            return _value;
        }
    }
    protected  bool IsDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatMod> statMods;
    public readonly ReadOnlyCollection<StatMod> StatMod;
    
    
    public CharacterStat()
    {
        
        statMods = new List<StatMod>();
        StatMod = statMods.AsReadOnly();
    }

    public CharacterStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }



    public virtual void AddMod(StatMod mod)
    {
        IsDirty = true;
        statMods.Add(mod);
        statMods.Sort(CompareModOrder);
    }

    protected virtual int CompareModOrder(StatMod a, StatMod b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public virtual bool RemoveMod(StatMod mod)
    {
        if (statMods.Remove(mod))
        {
            IsDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statMods.Count - 1; i >= 0; i++)
        {
            if(statMods[i].Source == source)
            {
                IsDirty = true;
                didRemove = true;
                statMods.RemoveAt(i);
            }


        }

        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statMods.Count; i++)
        {
            StatMod mod = statMods[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += statMods[i].Value;

            }

            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if (i + 1 >= statMods.Count || statMods[i +1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }

            else if ( mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
            
        }

        return (float)Math.Round(finalValue, 4);
    }


}
