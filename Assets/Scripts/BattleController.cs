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

    public Character Player { get { return player; } }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    void Start()
    {
        
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

    private void PerformMove(Character performer, Character receiver, int moveIndex)
    {
        Move move = performer.Moves[moveIndex];
        if (move.AttemptMove())
            receiver.ReceiveMove(move);

        OnMovePerformed?.Invoke();
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();

        // TODO: calculate initiative
        PerformMove(player, enemy, moveIndex);

        yield return new WaitForSeconds(1f);

        PerformMove(enemy, player, Random.Range(0, enemy.Moves.Count));

        OnBattleSequenceEnd?.Invoke();
    }
}
