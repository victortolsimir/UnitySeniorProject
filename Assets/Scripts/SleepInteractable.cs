using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInteractable : Interactable
{
    [SerializeField]
    private int cost = 50;

    [SerializeField]
    private ExclusionList enemyExclusions;

    private bool isSleeping = false;

    private void LateUpdate()
    {
        if (isSleeping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSleeping = false;
                SceneTransitionAnimation.PlayFadeIn();
                player.GetComponent<MyCharacterController>().EnableMovement();
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        CheckGold();
    }

    private void CheckGold()
    {
        var playerGold = Inventory.instance.inventorySO.gold;
        if (playerGold > cost)
        {
            Inventory.instance.inventorySO.gold -= cost;
            Inventory.instance.onItemChangedCallback?.Invoke();
            Sleep();
            RestoreEnemies();
        }
        else
        {
            MessagePanelSystem.ShowMessage($"Not enough gold. Renting this bed costs {cost} gold.");
        }
    }

    private void RestoreEnemies()
    {
        enemyExclusions.Clear();
    }

    private void Sleep()
    {
        MessagePanelSystem.ShowMessage("Sleeping...");
        SceneTransitionAnimation.PlayFadeOut();
        player.GetComponent<MyCharacterController>().DisableMovement();
        isSleeping = true;
        player.GetComponent<Character>().GainHealth(9999);
        player.GetComponent<Character>().GainMana(9999);
        player.GetComponent<Character>().GainStamina(9999);
    }
}
