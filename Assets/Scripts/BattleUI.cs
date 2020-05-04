﻿using System.Collections.Generic;
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

    private readonly List<Button> moveButtons = new List<Button>();

    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformedUI += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonsInteractable(true);

        foreach (Move move in battleController.PlayerCharacter.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.Name;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.PlayerCharacter.Moves.IndexOf(move)));
            moveButtons.Add(moveButton);
        }
    }

    private void SetMoveButtonsInteractable(bool set)
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
        playerHealthbar.value = battleController.PlayerCharacter.Health / battleController.PlayerCharacter.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
    }
}