using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RankTeams : MonoBehaviour
{
    public GameObject dataManagerObject;
    public GameObject rankCell;
    private DataManager dataManager;
    private Dropdown statDropdown;
    private Dropdown keyDropdown;
    private Text resultText;
    private string[] bools = { "Replay", "LeftWing", "AStop", "Feeder", "Spotlight", "Trap" };
    // Start is called before the first frame update
    void Start()
    {
        keyDropdown = transform.GetChild(0).GetComponent<Dropdown>();
        statDropdown = transform.GetChild(1).GetComponent<Dropdown>();
        resultText = transform.GetChild(2).GetComponent<Text>();
        dataManager = dataManagerObject.GetComponent<DataManager>();
        Get();
    }

    // Update is called once per frame
    public void Get()
    {
        
        string filepath = Application.persistentDataPath + $"/{PlayerPrefs.GetString("EventKey")}/obj";
        string stat = statDropdown.options[statDropdown.value].text;
        string key = keyDropdown.options[keyDropdown.value].text;
        if (stat == "None" || key == "None") { transform.GetChild(2).gameObject.SetActive(false); return; }
        transform.GetChild(2).gameObject.SetActive(true);


        Dictionary<string, float> rankings = new Dictionary<string, float>();
        Dictionary<string, float> deviations = new Dictionary<string, float>();


        if (stat == "Average (Deviation)")
        {

            foreach (var team in Directory.GetFiles(filepath))
            {

                // Displays a "Not Supported" Message as the only rank (Standard deviation isn't really a thing with booleans)
                if (bools.Any(key.Contains)) {
                    if (transform.GetChild(2).GetChild(0).GetChild(0).childCount > 0)
                    {
                        for (int i = 0; i < transform.GetChild(2).GetChild(0).GetChild(0).childCount; i++)
                        {

                            Destroy(transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).gameObject);

                        }
                    }
                    GameObject newRankCell = rankCell;
                    newRankCell.transform.GetChild(0).GetComponent<Text>().text = ":(";
                    newRankCell.transform.GetChild(1).GetComponent<Text>().text = "Not Supported";
                    newRankCell.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "";
                    newRankCell = Instantiate(newRankCell, transform.GetChild(2).GetChild(0).GetChild(0));
                    return;
                }


                else
                { 
                    string unparsedDeviation = dataManager.DataStats(team, key, stat);
                    rankings.Add(dataManager.MiscStats(team, "teamNum"), float.Parse(unparsedDeviation.Split(new string[] { " ± " }, StringSplitOptions.None)[0])); // This nightmare of a line gets the average without deviation
                    deviations.Add(dataManager.MiscStats(team, "teamNum"), float.Parse(unparsedDeviation.Split(new string[] { " ± " }, StringSplitOptions.None)[1])); // This nightmare of a line gets the deviation
                }
               
            }


        }
        // Sort for anything BUT standard deviation
        else
        {
            
            foreach (var team in Directory.GetFiles(filepath))
            {
                if (bools.Any(key.Contains)) { rankings.Add(dataManager.MiscStats(team, "teamNum"), float.Parse(dataManager.DataStats(team, key, stat, true))); }
                else
                {
                    rankings.Add(dataManager.MiscStats(team, "teamNum"), float.Parse(dataManager.DataStats(team, key, stat)));
                }
            }
           // rankings.Add("948", 3.5f);
           // rankings.Add("1678", 4.5f);

        }
        
            rankings = rankings.OrderByDescending(kv => kv.Value).ToList().ToDictionary(kv => kv.Key, kv => kv.Value);

        // Destroys any existing ranking cells (transform.GetChild(2).GetChild(0).GetChild(0) is the "Content" object of the scroll view)
        if (transform.GetChild(2).GetChild(0).GetChild(0).childCount > 0)
        {
            for (int i = 0; i < transform.GetChild(2).GetChild(0).GetChild(0).childCount; i++)
            {

                Destroy(transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).gameObject);
                
            }
        }


        // Creates new ranking cells
        int count = 1;
        foreach (var ranking in rankings)
        {
            GameObject newRankCell = rankCell;
            newRankCell.transform.GetChild(0).GetComponent<Text>().text = ranking.Key; // Team Number

            if (stat == "Average (Deviation)")
            {
                newRankCell.transform.GetChild(1).GetComponent<Text>().text = ranking.Value.ToString() + " ± " + deviations[ranking.Key]; // Value with deviation
            } else
            {
                newRankCell.transform.GetChild(1).GetComponent<Text>().text = ranking.Value.ToString(); // Value without deviation

            }
            newRankCell.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = count.ToString(); // Rank

            if (count == 1) { newRankCell.transform.GetChild(2).GetComponent<RawImage>().color = new Color(1.0f, 0.866f, 0.0f); } // Gold medal for 1st place
            if (count == 2) { newRankCell.transform.GetChild(2).GetComponent<RawImage>().color = new Color(0.745098f, 0.745098f, 0.745098f); } // Silver medal for 2nd place
            if (count == 3) { newRankCell.transform.GetChild(2).GetComponent<RawImage>().color = new Color(0.8f, 0.44706f, 0.03529f); }
            if (count > 3) { newRankCell.transform.GetChild(2).GetComponent<RawImage>().color = new Color(1.0f, 1.0f, 1.0f,0.0f); }

            newRankCell = Instantiate(newRankCell, transform.GetChild(2).GetChild(0).GetChild(0));
            count++;
            }

        
    }
}
