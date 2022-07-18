using System.Collections;
using System.Collections.Generic;

public class BattleTurnHandler
{
    public int PlayerActions { get; private set; }
    public int EnemyActions { get; private set; }
    public bool IsPlayerFirst { get; }

    private Stat playerSpeed;
    private Stat enemySpeed;
    private int playerBonus = 0;
    private int enemyBonus = 0;

    public BattleTurnHandler(Stat playerSpeed, Stat enemySpeed)
    {
        this.playerSpeed = playerSpeed;
        this.enemySpeed = enemySpeed;
        IsPlayerFirst = playerSpeed.GetValue() >= enemySpeed.GetValue();
    }

    public void CalculatePlayerActions()
    {
        playerBonus += playerSpeed.GetValue() - enemySpeed.GetValue();
        if (playerBonus < 0) playerBonus = 0;
        int bonusActions = playerBonus / enemySpeed.GetValue();
        if (bonusActions > 0)
        {
            playerBonus -= bonusActions * enemySpeed.GetValue();
        }
        PlayerActions = 1 + bonusActions;
    }

    public void CalculateEnemyActions()
    {
        enemyBonus += enemySpeed.GetValue() - playerSpeed.GetValue();
        if (enemyBonus < 0) enemyBonus = 0;
        int bonusActions = enemyBonus / playerSpeed.GetValue();
        if (bonusActions > 0)
        {
            enemyBonus -= bonusActions * playerSpeed.GetValue();
        }
        EnemyActions = 1 + bonusActions;
    }

    public void UsePlayerAction()
    {
        PlayerActions--;
    }

    public void UseEnemyAction()
    {
        EnemyActions--;
    }
}
