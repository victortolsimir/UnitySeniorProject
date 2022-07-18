using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestBossInteract : Interactable
{
    [SerializeField]
    private LootTable table;

    [SerializeField]
    private GameObject lootBagPrefab;

    public UniqueID uniqueID;
    public ExclusionList exclusionList;
    public GameObject mimicObject;
    public int mimicChance = 0;

    public UniqueID bossUniqueID;
    public ExclusionList bossExclusionList;

    private bool opened;
    private Animator chestAnimator;
    private GameObject chestLight;

    private void Start()
    {
        chestAnimator = GetComponentInChildren<Animator>();
        chestLight = gameObject.transform.GetChild(1).gameObject;

        // If it hasn't been opened
        if (!exclusionList.exclusions.Contains(uniqueID))
        {
            chestAnimator.SetBool("Open", false);
            chestLight.SetActive(false);
            opened = false;
        }
        else
        // If it has been opened
        {
            chestAnimator.SetBool("Open", true);
            chestLight.SetActive(true);
            opened = true;
        }
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius && !opened)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/chest_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public override void Interact()
    {
        if (!opened && bossExclusionList.exclusions.Contains(bossUniqueID))
        {
            if (Random.Range(0, 101) <= mimicChance)
            {
                opened = true;
                exclusionList.Add(uniqueID);
                BattleTrigger();
            }
            else
            {
                chestAnimator.SetBool("Open", true);
                chestLight.SetActive(true);
                opened = true;

                //Item item = table.GetItemFromTable();
                //item.DropItemObject(transform.position + transform.rotation * new Vector3(0, 0, 1));

                var items = table.GetItemsFromTable();
                if (items.Count > 0)
                {
                    var lootBag = Instantiate(lootBagPrefab, transform.position + transform.rotation * new Vector3(0, 0, 1), Quaternion.identity).GetComponent<LootBag>();
                    lootBag.Items = items;
                }

                exclusionList.Add(uniqueID);
            }
        }
        else if (!bossExclusionList.exclusions.Contains(bossUniqueID))
        {
            MessagePanelSystem.ShowMessage("An enemy must be killed to open this chest.");
        }
    }

    public void BattleTrigger()
    {
        GlobalControl.instance.playerstate = GlobalControl.PlayerState.EnteringBattle;
        GlobalControl.instance.sceneBeforeBattle = SceneManager.GetActiveScene().name;
        GlobalControl.instance.battleInfo.EnemyEncountered(uniqueID, exclusionList, mimicObject, table);
        GlobalControl.instance.battleInfo.enemyPosition = transform.position + transform.rotation * new Vector3(0, 0, 1);
        // SceneLoader.Load("Battle");
        SceneLoader.Load(BattleSceneInfo.GetBattleScene());
    }
}
