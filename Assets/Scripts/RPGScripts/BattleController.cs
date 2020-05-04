using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;
    public event System.Action OnWalk;
    public event System.Action OnDeath;

    public AudioClip dynamaxSound;
    public AudioClip attackClip;
    public GameObject cameraView;
    

    [SerializeField]
    private Character currentPlayer;

    [SerializeField]
    Transform playerSpawn;

    private Transform originalSize;

    public Character Player { get { return currentPlayer; }
                            set { currentPlayer = value; } }

    [SerializeField]
    Character enemy;

    [SerializeField]
    Transform enemySpawn;

    private float scale = 2.5f;
    private float moveDuration = 2.5f;
    float offSetPosition = 0.75f;
    public Character Enemy { get { return enemy; } }
    bool isPlayerMoving = false;
    bool isPlayerReturning = false;
    void Start()
    {

       
    }

    public IEnumerator PlayerToEnemy( float seconds, int moveIndex)
    {
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, enemySpawn.position * offSetPosition, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
          
        }
        PerformMove(currentPlayer, enemy, moveIndex);
        yield return new WaitForSeconds(1f);
        Player.transform.position = playerSpawn.position;

    }
    public IEnumerator EnemyToPlayer(float seconds, int enemyMoveIndex)
    {
       // OnWalk?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
     
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, playerSpawn.transform.position * offSetPosition, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PerformMove(enemy, currentPlayer, enemyMoveIndex);
        yield return new WaitForSeconds(1f);
        enemy.transform.position = enemySpawn.position;
    }

   

    private void PerformMove(Character performer, Character receiver, int moveIndex)
    {       
        Move move = performer.Moves[moveIndex];
        
        performer.PerformMove(moveIndex);

        if (move.AttemptMove())
        {
            AudioSource.PlayClipAtPoint(attackClip,performer.gameObject.transform.position);
            OnMovePerformed?.Invoke();
            receiver.ReceiveMove(move, receiver.GetCharType, performer.BonusDamage);
            receiver.GetComponent<ParticleSystem>().Play();
        }
    }

    public void PerformPlayerMove(int moveIndex)
    {
     
        if (moveIndex == 4)
        {
            FindObjectOfType<PartyUI>().TurnOnPanel();
            return;
        }
        if(moveIndex == 3)
        {
            Move Dynamaxmove = currentPlayer.Moves[moveIndex];
            Dynamaxmove.AttemptMove();
            originalSize = currentPlayer.gameObject.transform;
            currentPlayer.gameObject.transform.localScale += new Vector3(scale, scale, scale);
            currentPlayer.GetDynaMode = true;
            AudioSource.PlayClipAtPoint(dynamaxSound, currentPlayer.gameObject.transform.position);

            return;
        }
        if (currentPlayer.GetDynaDuration == 0)
        {
            currentPlayer.BonusDamage = 1;
            currentPlayer.GetDynaDuration = 2;
            currentPlayer.GetDynaMode = false;
            currentPlayer.gameObject.transform.localScale -= new Vector3(scale, scale, scale);
            return;
        }

        StartCoroutine(BattleSequence(moveIndex));
    }

    IEnumerator BattleSequence(int moveIndex)
    {
        yield return new WaitForSeconds(0.3f);
        cameraView.GetComponent<RotateCamera>().ToggleBattleView = true;
        OnBattleSequenceBegin?.Invoke();
        int enemyMoveIndex = Random.Range(0, enemy.Moves.Count - 1);
        //Debug.Log(Player.GetComponentInChildren<Animator>().);
        float playeranim = currentPlayer.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length;
        float enemyAnim = enemy.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length;
        //Debug.Log("Player Animation Time: " + playeranim);
       // Debug.Log("Enemy Animation Time: " + enemyAnim);
       if(currentPlayer.GetDynaMode)
        {
            currentPlayer.BonusDamage = 3;
            currentPlayer.GetDynaDuration--;
        }

        if (currentPlayer.GetSpeed + currentPlayer.Moves[moveIndex].GetMoveSpeed >= enemy.GetSpeed + enemy.Moves[enemyMoveIndex].GetMoveSpeed)
        {
            StartCoroutine(PlayerToEnemy(moveDuration, moveIndex));            
            yield return new WaitForSeconds(enemyAnim + moveDuration);
            if (enemy.isDead())
                OnBattleSequenceEnd?.Invoke();
            else
            StartCoroutine(EnemyToPlayer(moveDuration, enemyMoveIndex));     
        }
        else
        {
            StartCoroutine(EnemyToPlayer(moveDuration, enemyMoveIndex));
            yield return new WaitForSeconds(enemyAnim + moveDuration);
            if (currentPlayer.isDead())
                OnBattleSequenceEnd?.Invoke();
            else
               StartCoroutine(PlayerToEnemy(moveDuration, moveIndex));
              
        }
     
        yield return new WaitForSeconds(3f);
        OnBattleSequenceEnd?.Invoke();
        cameraView.GetComponent<RotateCamera>().ToggleBattleView = false;

    }
}
