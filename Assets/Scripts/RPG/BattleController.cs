using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSeqeunceEnd; 

    [SerializeField]
     Character player;

    public Character GetPlayer { get { return player; } }

    [SerializeField]
    Character enemy;

    public Character GetEnemy { get { return enemy; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            PerformMove(player, enemy, 0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(player, enemy, 1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PerformMove(player, enemy, 2);


    }

    private void PerformMove(Character performer,Character reciever, int moveIndex)
    {
        Move move = performer.moveList[moveIndex];
        if (move.AttemptMove())
        {
            reciever.RecieveMove(move);
        }
        OnMovePerformed?.Invoke();
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();
        // TODO: Calculate initiative
        PerformMove(player, enemy, moveIndex);

        yield return new WaitForSeconds(1f);
        
        PerformMove(enemy, player, Random.Range(0, enemy.moveList.Count));
        OnBattleSeqeunceEnd.Invoke();
    }
}
