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
    Text playerType;

    public Text EnemyName;
    Text EnemyType;

    private BattleController battleController;


    private List<Button> moveButtons = new List<Button>();

    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonsInteractable(true);

        foreach(Move move in battleController.Player.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.Name + " " + move.GetEnergy;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.Player.Moves.IndexOf(move)));
            moveButtons.Add(moveButton);
        }
        playerName.text = battleController.Player.propName;
        EnemyName.text = battleController.Enemy.propName;
    }

    private void SetMoveButtonsInteractable(bool set)
    {
        foreach (Button button in moveButtons)
            button.interactable = set;
    }

    void Update()
    {
        for(int i = 0; i < moveButtons.Count; ++i)
        {
            moveButtons[i].GetComponentInChildren<Text>().text = battleController.Player.Moves[i].Name + "\n " 
            + battleController.Player.Moves[i].GetEnergy + "/" + battleController.Player.Moves[i].GetMaxEnergy; 
        }
    }

    private void UpdateUI()
    {
        playerHealthbar.value = battleController.Player.Health / battleController.Player.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
    }
}
