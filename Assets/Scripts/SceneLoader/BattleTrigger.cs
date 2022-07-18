using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public GameObject prefab;
    public float collisionRadius = 1f;
    private GameObject player;

    [SerializeField]
    private LootTable lootTable;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalControl.instance.playerstate = GlobalControl.PlayerState.EnteringBattle;
            GlobalControl.instance.sceneBeforeBattle = SceneManager.GetActiveScene().name;
            Spawner spawner = GetComponentInParent<Spawner>();
            GlobalControl.instance.battleInfo.EnemyEncountered(spawner.uniqueID, spawner.exclusionList, prefab);
            GlobalControl.instance.battleInfo.enemyPosition = transform.position;
            SceneLoader.Load("Battle");
        }
    }*/

    public void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < collisionRadius)
            OnCollisionTrigger();
    }

    public void OnCollisionTrigger()
    {
        GlobalControl.instance.playerstate = GlobalControl.PlayerState.EnteringBattle;
        GlobalControl.instance.sceneBeforeBattle = SceneManager.GetActiveScene().name;
        Spawner spawner = GetComponentInParent<Spawner>();
        GlobalControl.instance.battleInfo.EnemyEncountered(spawner.uniqueID, spawner.exclusionList, prefab, lootTable);
        GlobalControl.instance.battleInfo.enemyPosition = transform.position;
        // SceneLoader.Load("Battle");
        if (spawner is BossSpawner bossSpawner)
        {
            SceneLoader.Load(bossSpawner.BossSceneName);
        }
        else
        {
            SceneLoader.Load(BattleSceneInfo.GetBattleScene());
        }
    }
}
