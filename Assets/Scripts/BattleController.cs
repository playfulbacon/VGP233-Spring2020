using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;

    [SerializeField]
    Character player;

    public Character Player { get { return player; } set { player = value; } }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(player, enemy, 0, 1.0f);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(player, enemy, 1, 1.0f);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PerformMove(player, enemy, 2, 1.0f);
    }

    private IEnumerator PerformMove(Character performer, Character receiver, int moveIndex, float mulitplier)
    {
        Move move = performer.Moves[moveIndex];
        if (performer.Health <= 0 || receiver.Health <= 0 || move.Energy <= 0)
        {
            yield break;
        }
        performer.StartMovement();
        yield return new WaitUntil(() => performer.IsMoving == false);
        performer.PerformMove(moveIndex);
        float attackTime = performer.GetComponent<CharacterAnimationController>().GetAnimationLength(move.AnimationName);
        yield return new WaitForSeconds(attackTime);
        if (move.AttemptMove())
            receiver.ReceiveMove(move,mulitplier);
        performer.StartMovement();
        yield return new WaitUntil(() => performer.IsMoving == false);
        OnMovePerformed?.Invoke();
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    private IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();

        // TODO: calculate initiative
        int enemyMoveIndex = Random.Range(0, enemy.Moves.Count);
        bool isPlayerFirst = true;
        float enemyMulitplier = CalculateMulitplier(enemy.Moves[enemyMoveIndex].Type,player.PokemoType);
        float playerMulitplier = CalculateMulitplier(player.Moves[moveIndex].Type, enemy.PokemoType);
        Debug.Log("enemy: " + enemyMoveIndex);
        Debug.Log("Player: " + moveIndex);
        if (player.Moves[moveIndex].Speed + player.Speed < enemy.Moves[enemyMoveIndex].Speed + enemy.Speed)
        {
            isPlayerFirst = false;
        }
        if (isPlayerFirst)
        {
            yield return PerformMove(player, enemy, moveIndex,playerMulitplier);
            //yield return new WaitForSeconds(1f);
            yield return PerformMove(enemy, player, enemyMoveIndex,enemyMulitplier);
        }
        else
        {
            yield return PerformMove(enemy, player, enemyMoveIndex, enemyMulitplier);
            //yield return new WaitForSeconds(1f);
            yield return PerformMove(player, enemy, moveIndex,playerMulitplier);
        }
        OnBattleSequenceEnd?.Invoke();
    }

    float CalculateMulitplier(GameConstants.Type attackType, GameConstants.Type defenderType)
    {
        if (attackType == GameConstants.Type.Fire && defenderType == GameConstants.Type.Grass)
        {
            return 1.2f;
        }
        if (attackType < defenderType)
        {
            return 1.2f;
        }
        else if(attackType > defenderType)
        {
            return 0.8f;
        }
        return 1.0f;
    }
}
