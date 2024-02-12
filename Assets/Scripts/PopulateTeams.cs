using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSidebar : MonoBehaviour
{
    public string[] teams;
    private string filepath;
    private string team;
    public GameObject dataManagerObject;
    private DataManager dataManager;
    public GameObject teamCellPrefab;
    public GameObject noTeamPrefab;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        filepath = Application.persistentDataPath + $"/{PlayerPrefs.GetString("EventKey")}/obj";
        if (!Directory.Exists(filepath)) { Instantiate(noTeamPrefab, transform.parent);return; }
        teams = Directory.GetFiles(filepath);
        foreach (var i in teams)
        {
            GameObject newTeamCell = teamCellPrefab;
            newTeamCell.transform.GetChild(1).GetComponent<Text>().text = dataManager.MiscStats(i, "teamNum");  
            newTeamCell.transform.GetChild(2).GetComponent<Text>().text = $"Total Matches: {dataManager.MiscStats(i, "length")}";
            newTeamCell.transform.GetChild(3).GetComponent<Text>().text = $"Data Quality: {dataManager.DataStats(i, "DataQuality", "Average", false).Truncate(4, "")}";
            newTeamCell = Instantiate(newTeamCell, transform.parent);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
