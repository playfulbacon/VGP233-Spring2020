using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceStart;
    public event System.Action OnBattleSequenceEnd;
    public event System.Action<Character> OnFighterCaptured;

    [SerializeField]
    Character player;

    public Character Player { get { return player; } }

    private List<Character> playerTeam = new List<Character>();
    public List<Character> PlayerTeam { get { return playerTeam; } }
    private List<Item> playerItemList = new List<Item>();
    public List<Item> PlayerItemList { get { return playerItemList; } }


    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    private List<Character> enemyTeam = new List<Character>();

    //public List<Character> EnemyFighterList { get { return enemyFighterList; } }

    void Awake()
    {
        playerTeam.Add(player);
        Item capture = new Item("Bootleg Pokeball", Item.Effect.Capture);
        playerItemList.Add(capture);
        enemyTeam.Add(enemy);

        // Adding more fighter to player team
        playerTeam.Add(new Character("Characmander"));
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(player, enemy, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(player, enemy, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PerformMove(player, enemy, 2);
    }

    private void PerformMove(Character attacker, Character defender, int moveIndex)
    {
        Move move = attacker.Moves[moveIndex];

        if (move.AttemptMove())
            defender.ReceiveMove(move);

        OnMovePerformed?.Invoke();
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceStart?.Invoke();

        // Calculate initiative
        int enemyMoveIndex = Random.Range(0, enemy.Moves.Count);

        if ((player.Speed + player.Moves[moveIndex].Speed) > (enemy.Speed + enemy.Moves[enemyMoveIndex].Speed))
        {
            PerformMove(player, enemy, moveIndex);
            yield return new WaitForSeconds(1f);
            PerformMove(enemy, player, enemyMoveIndex);
        }
        else
        {
            PerformMove(enemy, player, enemyMoveIndex);
            PerformMove(player, enemy, moveIndex);
        }

        OnBattleSequenceEnd?.Invoke();
    }

    public void ChangeCharacter(Character switchTo)
    {
        player = switchTo;
    }

    public void UseItem(Character target, Item item)
    {
        if (item.mEffect == Item.Effect.Capture)
        {
            playerTeam.Add(target);
            OnFighterCaptured?.Invoke(target);
        }
    }
}
