using System;
using System.Collections;
using System.Collections.Generic;

public class BattleAI
{
    private Random RandomGenerator { get; } = new Random();

    public int RandomAttackChoice(int attackCount)
    {
        if (RandomGenerator.Next(100) < 50) return 0;

        return RandomGenerator.Next(attackCount);
    }
}
