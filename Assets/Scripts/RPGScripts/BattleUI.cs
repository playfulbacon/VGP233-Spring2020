using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    Slider playerHealthbar;

    [SerializeField]
    Slider enemyHealthbar;

    [SerializeField]
    Button moveButtonPrefab;

    [SerializeField]
    Transform moveButtonsHolder;


    public Text playerName;
    public RawImage playerType;
    public Text DynamaxTxt;
    public Text DynamaxTurnTxt;
    public Text EnemyName;
    public RawImage EnemyType;

    private BattleController battleController;


    private List<Button> moveButtons = new List<Button>();

    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonsInteractable(true);
        Debug.Log(battleController.Player.propName);

        for (int i = 0; i < battleController.Player.Moves.Count; i++)
        {
            int tempI = i;
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text =
                battleController.Player.Moves[i].Name + " " + battleController.Player.Moves[i].GetEnergy;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(tempI));
            moveButtons.Add(moveButton);
        }

    }

    public void SetMoveButtonsInteractable(bool set)
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].interactable = set;
        }
       
    }

    public void SetSwitchButton(bool set)
    {
        moveButtons[moveButtons.Count].interactable = set;
    }

    void Update()
    {
        for (int i = 0; i < moveButtons.Count; ++i)
        {
            moveButtons[i].GetComponentInChildren<Text>().text = battleController.Player.Moves[i].Name + "\n "
            + battleController.Player.Moves[i].GetEnergy + "/" + battleController.Player.Moves[i].GetMaxEnergy;
        }
        playerName.text = battleController.Player.propName;
        EnemyName.text = battleController.Enemy.propName;
        UpdateTypeUI();
        UpdateDynamaxUI();
        UpdateUI();
    }

    private void UpdateTypeUI()
    {
        switch (battleController.Player.GetCharType)
        {
            case GameConstants.Type.Rock:
                playerType.color = Color.magenta;
                EnemyType.color = Color.magenta;

                break;
            case GameConstants.Type.Paper:
                playerType.color = Color.cyan;
                EnemyType.color = Color.cyan;

                break;
            case GameConstants.Type.Scissors:
                playerType.color = Color.green;
                EnemyType.color = Color.green;
                break;
        }
        switch (battleController.Enemy.GetCharType)
        {
            case GameConstants.Type.Rock:
                EnemyType.color = Color.magenta;
                break;
            case GameConstants.Type.Paper:
                EnemyType.color = Color.cyan;
                break;
            case GameConstants.Type.Scissors:
                EnemyType.color = Color.green;
                break;
        }
    }

    private void UpdateDynamaxUI()
    {
        if (battleController.Player.GetDynaMode)
        {
            DynamaxTxt.enabled = true;
            DynamaxTxt.gameObject.SetActive(true);
            DynamaxTurnTxt.enabled = true;
            DynamaxTurnTxt.gameObject.SetActive(true);
            DynamaxTurnTxt.text = battleController.Player.GetDynaDuration.ToString();
        }
        else
        {
            DynamaxTxt.enabled = false;
            DynamaxTxt.gameObject.SetActive(false);
            DynamaxTurnTxt.enabled = false;
            DynamaxTurnTxt.gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {
   

        playerHealthbar.value = battleController.Player.Health / battleController.Player.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
    }
}
