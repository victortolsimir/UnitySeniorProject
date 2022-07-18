using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarTest : MonoBehaviour
{

    [SerializeField] private float playerHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthImage;

    [SerializeField] private int damage;

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            playerHealth -= damage;
            UpdateHealth();
        }
        
        if (Input.GetKeyDown("h"))
        {
            playerHealth += damage;
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        healthImage.fillAmount = playerHealth / maxHealth;
    }
}
