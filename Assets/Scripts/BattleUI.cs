using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UIState
{
    Main,
    Battle,
    Fighters
}

public class BattleUI : MonoBehaviour
{
    private UIState uiState = UIState.Main;

    [SerializeField]
    Transform canvas;

    [SerializeField]
    Slider playerHealthbar;

    [SerializeField]
    Text playerNamePrefab;

    private Text playerName;

    [SerializeField]
    Slider enemyHealthbar;

    [SerializeField]
    Text enemyNamePrefab;

    private Text enemyName;

    [SerializeField]
    Transform mainButtonsHolder;

    [SerializeField]
    Transform moveButtonsHolder;

    [SerializeField]
    Transform fightersButtonsHolder;

    [SerializeField]
    Transform itemsButtonsHolder;

    [SerializeField]
    Button moveButtonPrefab;

    [SerializeField]
    Button returnButtonPrefab;

    private BattleController battleController;

    private List<Button> mainButtons = new List<Button>();
    private List<Button> moveButtons = new List<Button>();
    private List<Button> fightersButtons = new List<Button>();
    private List<Button> itemsButtons = new List<Button>();
    private Button returnButton;

    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceStart += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonsInteractable(true);
        battleController.OnFighterCaptured += AddToTeam;

        // Instantiate MainMenu
        Button fightButton = Instantiate(moveButtonPrefab, mainButtonsHolder);
        fightButton.GetComponentInChildren<Text>().text = "Fight";
        fightButton.onClick.AddListener(() => ShowMoveUI());
        mainButtons.Add(fightButton);
        Button pickFighterButton = Instantiate(moveButtonPrefab, mainButtonsHolder);
        pickFighterButton.GetComponentInChildren<Text>().text = "Change Fighter";
        pickFighterButton.onClick.AddListener(() => ShowFightersUI());
        mainButtons.Add(pickFighterButton);
        Button itemsButton = Instantiate(moveButtonPrefab, mainButtonsHolder);
        itemsButton.GetComponentInChildren<Text>().text = "Items";
        itemsButton.onClick.AddListener(() => ShowItemsUI());
        mainButtons.Add(itemsButton);

        // Instantiate Moves
        foreach (Move move in battleController.Player.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = string.Format("{0} - {1}/{2}", move.Name, move.Energy, move.MaxEnergy);
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.Player.Moves.IndexOf(move)));

            moveButtons.Add(moveButton);
        }
        moveButtonsHolder.gameObject.SetActive(false);

        // Instantiate Fighters
        foreach (Character fighter in battleController.PlayerTeam)
        {
            Button fighterButton = Instantiate(moveButtonPrefab, fightersButtonsHolder);
            fighterButton.GetComponentInChildren<Text>().text = fighter.Name;
            fighterButton.onClick.AddListener(() => battleController.ChangeCharacter(fighter));
            fighterButton.onClick.AddListener(() => ShowMainUI());
            fightersButtons.Add(fighterButton);
        }
        fightersButtonsHolder.gameObject.SetActive(false);

        // Instantiate Items
        foreach (Item item in battleController.PlayerItemList)
        {
            Button itemButton = Instantiate(moveButtonPrefab, itemsButtonsHolder);
            itemButton.GetComponentInChildren<Text>().text = item.mName;
            itemButton.onClick.AddListener(() => battleController.UseItem(battleController.Enemy, item));
            itemButton.onClick.AddListener(() => ShowMainUI());
            itemsButtons.Add(itemButton);
        }
        itemsButtonsHolder.gameObject.SetActive(false);

        // Instantiate Return Button
        returnButton = Instantiate(returnButtonPrefab, canvas);
        returnButton.onClick.AddListener(() => ShowMainUI());
        returnButton.gameObject.SetActive(false);

        // Instantiate Character Names
        playerName = Instantiate(playerNamePrefab, canvas);
        playerName.text = battleController.Player.Name;
        enemyName = Instantiate(enemyNamePrefab, canvas);
        enemyName.text = battleController.Enemy.Name;
    }

    private void SetMoveButtonsInteractable(bool set)
    {
        foreach (Button button in moveButtons)
            button.interactable = set;
    }
    
    void Update()
    {
        // Names
        playerName.text = battleController.Player.Name;
        enemyName.text = battleController.Enemy.Name;
    }

    private void UpdateUI()
    {
        playerHealthbar.value = battleController.Player.Health / battleController.Player.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
        // Update Energy Spent
        for (int i = 0; i < battleController.Player.Moves.Count; ++i)
        {
            moveButtons[i].GetComponentInChildren<Text>().text = string.Format("{0} - {1}/{2}",
                battleController.Player.Moves[i].Name, battleController.Player.Moves[i].Energy, battleController.Player.Moves[i].MaxEnergy);
        }
    }

    private void ShowMainUI()
    {
        returnButton.gameObject.SetActive(false);
        mainButtonsHolder.gameObject.SetActive(true);
        if (moveButtonsHolder.gameObject.activeSelf == true)
        {
            moveButtonsHolder.gameObject.SetActive(false);
        }
        if (fightersButtonsHolder.gameObject.activeSelf == true)
        {
            fightersButtonsHolder.gameObject.SetActive(false);
        }
        if (itemsButtonsHolder.gameObject.activeSelf == true)
        {
            itemsButtonsHolder.gameObject.SetActive(false);
        }
    }

    private void ShowMoveUI()
    {
        returnButton.gameObject.SetActive(true);
        mainButtonsHolder.gameObject.SetActive(false);
        moveButtonsHolder.gameObject.SetActive(true);
    }

    private void ShowFightersUI()
    {
        returnButton.gameObject.SetActive(true);
        mainButtonsHolder.gameObject.SetActive(false);
        fightersButtonsHolder.gameObject.SetActive(true);
    }

    private void ShowItemsUI()
    {
        returnButton.gameObject.SetActive(true);
        mainButtonsHolder.gameObject.SetActive(false);
        itemsButtonsHolder.gameObject.SetActive(true);
    }

    private void AddToTeam(Character fighter)
    {
        Button fighterButton = Instantiate(moveButtonPrefab, fightersButtonsHolder);
        fighterButton.GetComponentInChildren<Text>().text = fighter.Name;
        fighterButton.onClick.AddListener(() => battleController.ChangeCharacter(fighter));
        fighterButton.onClick.AddListener(() => ShowMainUI());
        fightersButtons.Add(fighterButton);

    }
}
