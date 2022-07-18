using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestSO : ScriptableObject
{
    [SerializeField]
    private QuestSO _prerequisiteQuest;
    public QuestSO PrerequisiteQuest { get => _prerequisiteQuest; }

    [SerializeField]
    private Item _specialItem;

    public Item SpecialItem { get => _specialItem; }

    [SerializeField]
    private string _questName;
    public string QuestName { get => _questName; }

    [SerializeField]
    private string _description;
    public string Description { get => _description; }

    [SerializeField]
    private string _introQuestDialogue;
    public string IntroQuestDialogue { get => _introQuestDialogue; }

    [SerializeField]
    private string _activeQuestDialogue;
    public string ActiveQuestDialogue { get => _activeQuestDialogue; }

    [SerializeField]
    private string _completedQuestDialogue;
    public string CompletedQuestDialogue { get => _completedQuestDialogue; }

    [SerializeField]
    private int _goldReward = 0;
    public int GoldReward { get => _goldReward; }

    [SerializeField]
    private List<Item> _itemRewards = new List<Item>();
    public List<Item> ItemRewards
    {
        get
        {
            var list = new List<Item>();
            _itemRewards.ForEach(item => { list.Add(item); });
            return list;
        }
    }

    public abstract void EnemyKilled(QuestController.EnemyType enemyType);

    public virtual void Reset() { }

    public abstract bool IsComplete();

    public virtual string GetQuestDescription() => "";
}
