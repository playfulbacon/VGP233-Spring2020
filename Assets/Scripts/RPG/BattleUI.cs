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

    // Start is called before the first frame update
    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        FindObjectOfType<BattleController>().OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSeqeunceEnd += () => SetMoveButtonsInteractable(true);

        foreach (Move move in battleController.GetPlayer.moveList)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.GetName;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.GetPlayer.moveList.IndexOf(move)));
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        playerHealthbar.value = battleController.GetPlayer.GetCurrentHealth / battleController.GetPlayer.GetMaxHealth;
        enemyHealthbar.value = battleController.GetEnemy.GetCurrentHealth / battleController.GetEnemy.GetMaxHealth;
    }
}
