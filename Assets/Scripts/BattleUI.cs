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

    private BattleController battleController;

    private List<Button> moveButtons = new List<Button>();

    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonInteractable(true);

        foreach (Move move in battleController.Player.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.Name;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.Player.Moves.IndexOf(move)));
            moveButtons.Add(moveButton);
        }
    }

    private void SetMoveButtonInteractable(bool set)
    {
        foreach (Button button in moveButtons)
        {
            button.interactable = set;
        }
    }

    void Update()
    {

    }

    private void UpdateUI()
    {
        playerHealthbar.value = battleController.Player.Health / battleController.Player.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
    }
}
