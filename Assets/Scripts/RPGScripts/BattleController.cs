using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;

    public PartySystem partySystem;

    [SerializeField]
    private Character currentPlayer;

    public Character Player { get { return currentPlayer; }
                            set { currentPlayer = value; } }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    void Start()
    {

       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(currentPlayer, enemy, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(currentPlayer, enemy, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PerformMove(currentPlayer, enemy, 2);

        
    }

    private void PerformMove(Character performer, Character receiver, int moveIndex)
    {     
            Move move = performer.Moves[moveIndex];
    
        if (move.AttemptMove())
        {
            receiver.ReceiveMove(move, receiver.GetCharType);
            OnMovePerformed?.Invoke();
        }
    }

    public void PerformPlayerMove(int moveIndex)
    {
        if (moveIndex == 3)
        {
            FindObjectOfType<PartyUI>().TurnOnPanel();
            return;
        }
        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();
        int enemyMoveIndex = Random.Range(0, enemy.Moves.Count - 1);

        if (currentPlayer.GetSpeed + currentPlayer.Moves[moveIndex].GetMoveSpeed >= enemy.GetSpeed + enemy.Moves[enemyMoveIndex].GetMoveSpeed)
        {
            PerformMove(currentPlayer, enemy, moveIndex);
            yield return new WaitForSeconds(1f);
            PerformMove(enemy, currentPlayer, enemyMoveIndex);
        }
        else
        {
            PerformMove(enemy, currentPlayer, enemyMoveIndex);
            yield return new WaitForSeconds(1f);
            PerformMove(currentPlayer, enemy, moveIndex);
        }
        OnBattleSequenceEnd?.Invoke();
    }
}
