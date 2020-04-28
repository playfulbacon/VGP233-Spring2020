using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySystem : MonoBehaviour
{
    public event System.Action onSwitchin;

    public BattleController battlecontroller;
    [SerializeField]
    public GameObject characterPrefab;
    [SerializeField]
    public List<GameObject> partyList = new List<GameObject>();
    public Transform playerspawn;

    private int previousIndex = 0;
    public int playerIndex { get { return previousIndex; } }
    [SerializeField]
    int PartySize = 5;

    void Awake()
    {
        PopulateParty();
        battlecontroller.Player = partyList[previousIndex].gameObject.GetComponent<Character>();
    }


    void PopulateParty()
    {
        for(int i = 0; i < PartySize; i++ )
        {            
            partyList.Add(Instantiate(characterPrefab, playerspawn));
        }
        for(int i = 1; i < PartySize; i++)
        {
            partyList[i].SetActive(false);
        }
    }

    public void PerformSwitchIn(int selectIndex)
    {
        partyList[previousIndex].SetActive(false);
        previousIndex = selectIndex;
        partyList[previousIndex].SetActive(true);
        battlecontroller.Player = partyList[previousIndex].gameObject.GetComponent<Character>();
        Debug.Log("Switching in " + partyList[previousIndex].gameObject.GetComponent<Character>().propName);
        FindObjectOfType<PartyUI>().GetComponent<PartyUI>().TurnOffPanel();
        onSwitchin?.Invoke();
    }



}
