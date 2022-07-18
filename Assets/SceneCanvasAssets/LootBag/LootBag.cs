﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : Interactable
{
    [SerializeField]
    private List<Item> _items;
    public List<Item> Items { get => _items; set => _items = value; }

    public override void Interact()
    {
        base.Interact();
        OpenLootBag();
    }

    private void OpenLootBag()
    {
        LootBagManager.OpenBag(this);
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/inventory_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}