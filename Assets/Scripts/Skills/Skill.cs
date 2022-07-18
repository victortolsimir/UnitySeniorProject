using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    [SerializeField]
    private int _level = 1;
    public int Level
    {
        get => _level;
        private set { if (value > _level) _level = value; }
    }

    [SerializeField]
    private int _experience = 0;
    public int Experience
    {
        get => _experience;
        private set { _experience = value; }
    }

    public int MaxLevel { get; } = 10;

    public int RequiredExperience()
    {
        if (Level < MaxLevel)
            return 100 + 20 * (Level - 1);
        else
            return 1000;
    }

    public void AddExperience(int xp)
    {
        if (Level < MaxLevel)
        {
            Experience += xp;
            while (Experience >= RequiredExperience())
            {
                Experience -= RequiredExperience();
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        Level++;
        ApplyLevelUpEffect();
        if (Level >= MaxLevel)
            Experience = RequiredExperience() - 1;
    }

    protected virtual void ApplyLevelUpEffect() { }
}
