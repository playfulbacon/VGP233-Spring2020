using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformedUI;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;

    [SerializeField]
    Character playerCharacter;

    public Character PlayerCharacter { get { return playerCharacter; } }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PerformMove(playerCharacter, enemy, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PerformMove(playerCharacter, enemy, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PerformMove(playerCharacter, enemy, 2);
        }
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    private IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();

        yield return PerformMove(playerCharacter, enemy, moveIndex);

        yield return PerformMove(enemy, playerCharacter, Random.Range(0, enemy.Moves.Count));

        OnBattleSequenceEnd?.Invoke();
    }

    private IEnumerator PerformMove(Character performer, Character receiver, int moveIndex)
    {
        if (!performer.IsDead && !receiver.IsDead)
        {
            Move move = performer.Moves[moveIndex];
            yield return MoveForward(performer, receiver, move);

            receiver.ReceiveMove(move);

            OnMovePerformedUI?.Invoke();
        }

    }

    private IEnumerator MoveForward(Character performer, Character enemy, Move move)
    {
        Vector3 orignalPost = performer.transform.position;
        while (performer.transform.position != enemy.transform.position * 0.5f)
        {
            performer.transform.position = Vector3.Lerp(performer.transform.position, enemy.transform.position * 0.5f, Time.deltaTime * 2.0f);

            yield return null;
        }
        float attackTime = performer.GetComponent<CharacterAnimationController>().GetAnimationLength(move.AnimationName);
        performer.PerformMove(move);
        yield return new WaitForSeconds(attackTime);
        performer.transform.position = orignalPost;
    }
}
