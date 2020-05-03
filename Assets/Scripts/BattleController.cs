using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;

    [SerializeField]
    Character playerCharacter;

    public Character PlayerCharacter { get { return playerCharacter; } }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(playerCharacter, enemy, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(playerCharacter, enemy, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PerformMove(playerCharacter, enemy, 2);
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    private IEnumerator BattleSequence(int moveIndex)
    {     
        OnBattleSequenceBegin?.Invoke();
        
        yield return MoveForward(PlayerCharacter);
        yield return PerformMove(playerCharacter, enemy, moveIndex);
        yield return MoveBackward(PlayerCharacter);

        yield return MoveForward(enemy);
        yield return PerformMove(enemy, playerCharacter, Random.Range(0, enemy.Moves.Count));
        yield return MoveBackward(enemy);

        OnBattleSequenceEnd?.Invoke();
    }

    private IEnumerator PerformMove(Character performer, Character receiver, int moveIndex)
    {
        Move move = performer.Moves[moveIndex];
        performer.PerformMove(moveIndex);
        float attackTime = performer.GetComponent<CharacterAnimationController>().GetAnimationLength(move.AnimationName);
        yield return new WaitForSeconds(attackTime);
        receiver.ReceiveMove(move);
        OnMovePerformed?.Invoke();
    }

    private IEnumerator MoveForward(Character toMove)
    {
        toMove.IsMovingForward = true;
        yield return new WaitForSeconds(1);
        toMove.IsMovingForward = false;
    }

    private IEnumerator MoveBackward(Character toMove)
    {
        toMove.IsMovingBackward = true;
        yield return new WaitForSeconds(1);
        toMove.IsMovingBackward = false;
    }
}
