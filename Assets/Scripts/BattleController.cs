using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;
    public event System.Action OnPerformMoves;

    [SerializeField]
    GameObject teamCharacters;

    [SerializeField]
    Character player;

    public Character Player
    {
        get => player;
        set => player = value;
    }

    [SerializeField]
    Character enemy;

    public Character Enemy { get { return enemy; } }

    private WaitForSeconds mWaitOneSecond;
    public List<Character> ListCharacters { get; private set; }

    private bool mSwitchPlayer = false;

    private void Awake()
    {
        ListCharacters = teamCharacters.transform.GetComponentsInChildren<Character>().ToList();
        ListCharacters[1].gameObject.SetActive(false);
    }

    void Start()
    {
        mWaitOneSecond = new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PerformMove(player, enemy, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PerformMove(player, enemy, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PerformMove(player, enemy, 2);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mSwitchPlayer = !mSwitchPlayer;
            ListCharacters[0].gameObject.SetActive(mSwitchPlayer);
            ListCharacters[1].gameObject.SetActive(!mSwitchPlayer);
        }

    }


    public void PerformMove(Character performed, Character receiver, int moveindex)
    {
        Move move = performed.Moves[moveindex];
        if (move.AttemptMove())
        {
            receiver.CheckCharacterType(performed, move);
            //receiver.ReceiveMove(move);
        }
        OnMovePerformed?.Invoke();
    }

    public void PerformPlayerMove(int moveIndex)
    {
        StartCoroutine(BattleSequence(moveIndex));
    }

    public void PerformPlayerMoves(int moveIndex)
    {

    }

    IEnumerator BattleSequence(int moveIndex)
    {
        OnBattleSequenceBegin?.Invoke();
        if (player.Speed > enemy.Speed)
        {
            PerformMove(player, enemy, moveIndex);
            yield return mWaitOneSecond;
            PerformMove(enemy, player, Random.Range(0, enemy.Moves.Count));
        }
        else
        {
            PerformMove(enemy, player, Random.Range(0, enemy.Moves.Count));
            yield return mWaitOneSecond;
            PerformMove(player, enemy, moveIndex);
        }
        OnBattleSequenceEnd?.Invoke();
    }

}
