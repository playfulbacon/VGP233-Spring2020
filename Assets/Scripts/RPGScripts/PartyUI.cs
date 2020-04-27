using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{    
    public Button PartyButtonPrefab;

    public Transform moveButtonsHolder;

    public GameObject panel;
    private PartySystem party;
    private List<Button> selectButtonList = new List<Button>();
    private void Start()
    {
        party = FindObjectOfType<PartySystem>();
        
        for (int i = 0; i < party.partyList.Count; i++)
        {
            //Apparently you need to make a temperary value when passing in 
            // a value into lambda https://answers.unity.com/questions/908847/passing-a-temporary-variable-to-add-listener.html
            int tempI = i;
            Button SelectPlayerButton = Instantiate(PartyButtonPrefab, moveButtonsHolder);
            SelectPlayerButton.GetComponentInChildren<Text>().text = 
                party.partyList[i].GetComponent<Character>().propName +
            "\n" + party.partyList[i].GetComponent<Character>().Health + "/" + 
            party.partyList[i].GetComponent<Character>().MaxHealth + "hp";
            SelectPlayerButton.onClick.AddListener(() => party.PerformSwitchIn(tempI));
            selectButtonList.Add(SelectPlayerButton);
        }
    }


    private void Update()
    {
        for (int i = 0; i < selectButtonList.Count; ++i)
        {
            selectButtonList[i].GetComponentInChildren<Text>().text = party.partyList[i].GetComponent<Character>().propName
            + "\n" + party.partyList[i].GetComponent<Character>().Health.ToString("F0") + "/" +
            party.partyList[i].GetComponent<Character>().MaxHealth + "hp";
        }
    }

    public void TurnOnPanel()
    {
        panel.SetActive(true);
    }
    public void TurnOffPanel()
    {
        panel.SetActive(false);
    }
}
