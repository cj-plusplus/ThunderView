using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankCell : MonoBehaviour
{
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager =  GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    // Update is called once per frame
    public void UpdateTeamView()
    {
        dataManager.TeamViewSetup(transform.GetChild(0).GetComponent<Text>().text);
    }
}
