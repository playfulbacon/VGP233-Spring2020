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

    [SerializeField]
    Dropdown characterList;

    [SerializeField]
    Button enemyCloneButton;

    private BattleController battleController;
    private List<Button> moveButtons = new List<Button>();
    private List<GameObject> characters = new List<GameObject>();
    public GameObject playerCharacters;
    void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        battleController.OnMovePerformed += UpdateUI;
        battleController.OnBattleSequenceBegin += () => SetMoveButtonsInteractable(false);
        battleController.OnBattleSequenceEnd += () => SetMoveButtonsInteractable(true);

        foreach(Move move in battleController.Player.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.Name;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.Player.Moves.IndexOf(move)));
            moveButtons.Add(moveButton);
        }

        foreach (var character in playerCharacters.GetComponentsInChildren<Character>())
        {
            characterList.AddOptions(new List<string> { character.name });
            character.gameObject.SetActive(false);
            characters.Add(character.gameObject);
        }
        battleController.Player = characters[0].GetComponent<Character>();
        characters[0].SetActive(true);
        characterList.onValueChanged.AddListener(switchCharacter);
        UpdateUI();
    }

    private void SetMoveButtonsInteractable(bool set)
    {
        foreach (Button button in moveButtons)
            button.interactable = set;
    }

    void Update()
    {
        
    }

    private void UpdateUI()
    {
        playerHealthbar.value = battleController.Player.Health / battleController.Player.MaxHealth;
        enemyHealthbar.value = battleController.Enemy.Health / battleController.Enemy.MaxHealth;
        foreach (var button in moveButtons)
        {
            Move move = battleController.Player.Moves[moveButtons.IndexOf(button)];
            button.GetComponentInChildren<Text>().text = move.Name + "Energy: "+ move .Energy.ToString();
        }
    }

    void switchCharacter(int index)
    {
        characters[index].SetActive(true);
        battleController.Player.gameObject.SetActive(false);
        battleController.Player = characters[index].GetComponent<Character>();
        ResetButtons();
        UpdateUI();
    }

    void ResetButtons()
    {
        foreach (var button in moveButtons)
        {
            Destroy(button.gameObject);
        }
        moveButtons.Clear();
        foreach (Move move in battleController.Player.Moves)
        {
            Button moveButton = Instantiate(moveButtonPrefab, moveButtonsHolder);
            moveButton.GetComponentInChildren<Text>().text = move.Name;
            moveButton.onClick.AddListener(() => battleController.PerformPlayerMove(battleController.Player.Moves.IndexOf(move)));
            moveButtons.Add(moveButton);
        }
    }

    public void Clone()
    {
        GameObject enemyClone = Instantiate(battleController.Enemy.gameObject);
        enemyClone.GetComponent<Character>().Health = battleController.Enemy.Health;
        for( int i = 0; i<battleController.Enemy.Moves.Count; ++i )
        {
            enemyClone.GetComponent<Character>().Moves[i] = battleController.Enemy.Moves[i];
        }
        characters.Add(enemyClone);
        characterList.AddOptions(new List<string> { enemyClone.name});
    }
}
