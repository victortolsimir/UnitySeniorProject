using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneInfo : MonoBehaviour
{
    private static BattleSceneInfo instance;

    [SerializeField]
    private string battleScene;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public static string GetBattleScene()
    {
        if (instance)
            return instance.battleScene;
        else
            return "";
    }
}
