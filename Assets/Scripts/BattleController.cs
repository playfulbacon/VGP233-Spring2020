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
    Character playerCharacter;

    public Character PlayerCharacter { get { return playerCharacter; } }

    private List<Character> playerTeam = new List<Character>();
    public List<Character> PlayerTeam { get { return playerTeam; } }
    private List<Item> playerItemList = new List<Item>();
    public List<Item> PlayerItemList { get { return playerItemList; } }


    [SerializeField]
    Character enemyCharacter;

    public Character EnemyCharacter { get { return enemyCharacter; } }

    private List<Character> enemyTeam = new List<Character>();

    //public List<Character> EnemyFighterList { get { return enemyFighterList; } }

    void Awake()
    {
        playerTeam.Add(playerCharacter);
        Item capture = new Item("Bootleg Pokeball", Item.Effect.Capture);
        playerItemList.Add(capture);
        enemyTeam.Add(enemyCharacter);

        // Adding more fighter to player team
        playerTeam.Add(new Character("Characmander"));
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(playerCharacter, enemyCharacter, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(playerCharacter, enemyCharacter, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PerformMove(playerCharacter, enemyCharacter, 2);
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceStart?.Invoke();

        // Calculate initiative
        int enemyMoveIndex = Random.Range(0, enemyCharacter.Moves.Count);

        if ((playerCharacter.Speed + playerCharacter.Moves[moveIndex].Speed) > (enemyCharacter.Speed + enemyCharacter.Moves[enemyMoveIndex].Speed))
        {
            float attackTime = playerCharacter.GetComponent<CharacterAnimationController>().GetAnimationLength("Attack");
            PerformMove(playerCharacter, enemyCharacter, moveIndex);
            yield return new WaitForSeconds(attackTime);
            PerformMove(enemyCharacter, playerCharacter, enemyMoveIndex);
        }
        else
        {
            PerformMove(enemyCharacter, playerCharacter, enemyMoveIndex);
            PerformMove(playerCharacter, enemyCharacter, moveIndex);
        }

        OnBattleSequenceEnd?.Invoke();
    }

    private void PerformMove(Character attacker, Character defender, int moveIndex)
    {
        attacker.PerformMove(moveIndex);

        Move move = attacker.Moves[moveIndex];

        if (move.AttemptMove())
            defender.ReceiveMove(move);

        OnMovePerformed?.Invoke();
    }

    public void ChangeCharacter(Character switchTo)
    {
        playerCharacter = switchTo;
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
