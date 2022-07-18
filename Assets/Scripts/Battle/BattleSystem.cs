using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, BUSY }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    private BattleCharacter player;
    private BattleCharacter enemy;

    [SerializeField]
    private BattleState state;

    [SerializeField]
    private BattleUI battleUI;

    [SerializeField]
    private InventorySO inventory;
    private List<Item> potions;
    private BattleTurnHandler turnHandler;
    private BattleAI enemyAI = new BattleAI();

    private void Start()
    {
        state = BattleState.START;
        potions = inventory.items["Potion"];
        SetupBattle();
    }

    private void Update()
    {
        if (state is BattleState.ENEMYTURN)
        {
            state = BattleState.BUSY;
            StartCoroutine(EnemyTurn());
        }
    }

    private void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerGO.name = "Player";
        player = playerGO.GetComponent<BattleCharacter>();
        player.onAttackComplete = PlayerActionUsed;
        player.onAttackFail = () => state = BattleState.PLAYERTURN;

        GameObject enemyGO = Instantiate(GlobalControl.instance.battleInfo.GetEnemy(), enemyBattleStation);
        enemyGO.name = "Enemy";
        enemy = enemyGO.GetComponent<BattleCharacter>();
        enemy.onAttackComplete = EnemyActionUsed;

        turnHandler = new BattleTurnHandler(player.stats.attackSpeed, enemy.stats.attackSpeed);

        SetupUI();

        if (turnHandler.IsPlayerFirst)
        {
            player.ShowFloatingText("First Strike");
            SwitchToPlayerTurn();
        }
        else
        {
            enemy.ShowFloatingText("First Strike");
            SwitchToEnemyTurn();
        }
    }

    private void SetupUI()
    {
        battleUI.AddAbilites(player.abilities, OnAbilityUse);
        battleUI.AddSpells(player.spells, OnSpellUse);
        battleUI.AddItems(potions, OnItemUse);
        battleUI.SetEscapeButton(OnEscapeAttempt);
    }

    private void UpdateActionsUI()
    {
        battleUI.UpdateActionText(turnHandler.PlayerActions, turnHandler.EnemyActions);
    }

    public void OnSpellUse(int choice)
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.BUSY;
            player.UseSpell(choice, enemy);
            battleUI.DisableExtraPanels();
        }
    }

    public void OnAbilityUse(int choice)
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.BUSY;
            player.UseAbility(choice, enemy);
            battleUI.DisableExtraPanels();

        }
    }

    //public void OnItemUse(int choice)
    //{
    //    if (state == BattleState.PLAYERTURN)
    //    {
    //        state = BattleState.BUSY;
    //        Item item = potions[choice];
    //        item.Use();
    //        inventory.totalWeight -= item.weight;
    //        potions.RemoveAt(choice);

    //        battleUI.DisableExtraPanels();

    //        battleUI.ClearItems();
    //        battleUI.AddItems(potions, OnItemUse);

    //        PlayerActionUsed();
    //    }
    //}

    public void OnItemUse(Item item)
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.BUSY;
            item.Use();
            inventory.totalWeight -= item.weight;
            // potions.Remove(item);
            potions.RemoveAt(potions.LastIndexOf(item));

            battleUI.DisableExtraPanels();

            battleUI.ClearItems();
            battleUI.AddItems(potions, OnItemUse);

            PlayerActionUsed();
        }
    }

    public void OnEscapeAttempt()
    {
        if (state == BattleState.PLAYERTURN)
        {
            player.ShowFloatingText("Coward!");
            player.onAttackComplete();
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1.25f);
        int abilityCount = enemy.abilities.Count;
        int spellCount = enemy.spells.Count;
        int enemyChoice = enemyAI.RandomAttackChoice(abilityCount + spellCount);
        if (enemyChoice < abilityCount)
        {
            enemy.UseAbility(enemyChoice, player);
        }
        else if (enemyChoice < abilityCount + spellCount)
        {
            enemy.UseSpell(enemyChoice - abilityCount, player);
        }
        else
        {
            enemy.UseAbility(0, player);
        }
    }

    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(0.5f);
        player.ClearEffects();
        battleUI.OnVictory();
        GlobalControl.instance.battleInfo.EnemyDefeated();
        yield return new WaitForSeconds(2.0f);
        ReturnFromBattle();
    }

    private IEnumerator Defeat()
    {
        yield return new WaitForSeconds(0.5f);
        battleUI.OnDefeat();
        yield return new WaitForSeconds(2.0f);
        SceneLoader.Load("MainMenu");
    }

    private void ReturnFromBattle()
    {
        GlobalControl.instance.playerstate = GlobalControl.PlayerState.ReturningFromBattle;
        // QuestController.instance.EnemyWasKilled(GlobalControl.instance.battleInfo.GetEnemyID(), GlobalControl.instance.battleInfo.GetUniqueID());
        QuestController.instance.EnemyWasKilled(GlobalControl.instance.battleInfo.GetEnemyID());
        SceneLoader.Load(GlobalControl.instance.sceneBeforeBattle);
    }

    private void PlayerActionUsed()
    {
        turnHandler.UsePlayerAction();
        UpdateActionsUI();

        if (enemy.IsDead())
        {
            state = BattleState.WON;
            StartCoroutine(Victory());
        }
        else if (turnHandler.PlayerActions > 0)
        {
            state = BattleState.PLAYERTURN;
        }
        else
        {
            SwitchToEnemyTurn();
        }
    }

    private void EnemyActionUsed()
    {
        turnHandler.UseEnemyAction();
        UpdateActionsUI();

        if (player.IsDead())
        {
            state = BattleState.LOST;
            StartCoroutine(Defeat());
        }
        else if (turnHandler.EnemyActions > 0)
        {
            state = BattleState.ENEMYTURN;
        }
        else
        {
            SwitchToPlayerTurn();
        }
    }

    private void SwitchToPlayerTurn()
    {
        enemy.HandleEffects();
        if (enemy.IsDead())
        {
            state = BattleState.WON;
            StartCoroutine(Victory());
        }
        else
        {
            turnHandler.CalculatePlayerActions();
            UpdateActionsUI();
            state = BattleState.PLAYERTURN;
        }
    }

    private void SwitchToEnemyTurn()
    {
        player.HandleEffects();
        if (player.IsDead())
        {
            state = BattleState.LOST;
            StartCoroutine(Defeat());
        }
        else
        {
            turnHandler.CalculateEnemyActions();
            UpdateActionsUI();
            state = BattleState.ENEMYTURN;
        }
    }
}
