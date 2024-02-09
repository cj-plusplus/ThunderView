using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AllianceMatchSpawner : MonoBehaviour
{
    public GameObject matchPrefab;
    public GameObject dataManagerObject;
    private DataManager dataManager;
    private string filepath;
    // Start is called before the first frame update
    void Start()
    {
        filepath = Application.persistentDataPath + "/2024cmptx/alliance";
        dataManager = dataManagerObject.GetComponent<DataManager>();
        SpawnMatches("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnMatches(string selective)
    {
        if (transform.parent.childCount > 1)
        {

            for (int i = 1; i <  transform.parent.childCount; i++)
            { 
                Destroy(transform.parent.GetChild(i).gameObject);
            }
        }
        foreach (var match in Directory.GetFiles(filepath))
        {
            DataManager.AllianceMatch matchJson = JsonUtility.FromJson<DataManager.AllianceMatch>(File.ReadAllText(match));
            if (selective != "")
            {
                if (!(matchJson.Team1.ToString() == selective || matchJson.Team2.ToString() == selective || matchJson.Team3.ToString() == selective)) {  continue; }
            }
            GameObject newMatchCell = matchPrefab;
            newMatchCell.transform.GetChild(0).GetComponent<Text>().text = matchJson.MatchNumber.ToString();
            newMatchCell.transform.GetChild(1).GetComponent<Text>().text = matchJson.MatchType;
            newMatchCell.transform.GetChild(2).GetComponent<Text>().text = $"Data Quality: {matchJson.DataQuality.ToString()}/5";
            newMatchCell.transform.GetChild(3).GetComponent<Text>().text = $"Scouter Name: {matchJson.ScouterName}";
            newMatchCell.transform.GetChild(4).gameObject.SetActive(matchJson.Replay);
            newMatchCell.transform.GetChild(5).GetComponent<Text>().text = matchJson.Team1.ToString();
            newMatchCell.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = $"Defense: {matchJson.Team1Defense}/5\nDriver Skill: {matchJson.Team1DriverSkill}/5";
            newMatchCell.transform.GetChild(6).GetComponent<Text>().text = matchJson.Team2.ToString();
            newMatchCell.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = $"Defense: {matchJson.Team2Defense}/5\nDriver Skill: {matchJson.Team2DriverSkill}/5";
            newMatchCell.transform.GetChild(7).GetComponent<Text>().text = matchJson.Team3.ToString();
            newMatchCell.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = $"Defense: {matchJson.Team3Defense}/5\nDriver Skill: {matchJson.Team3DriverSkill}/5";
            newMatchCell.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = $"Auto Center Notes: {matchJson.AutoCenterNotes}";
            newMatchCell.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = $"High Notes: {matchJson.HighNotes}/{matchJson.HighNotePotential}";
            newMatchCell.transform.GetChild(8).GetChild(2).GetComponent<Text>().text = $"Amplify Count: {matchJson.AmplifyCount}";
            newMatchCell.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = $"Team At Amp: {matchJson.TeamAtAmp}";
            newMatchCell.transform.GetChild(8).GetChild(4).GetComponent<Text>().text = $"Bots in Harmony: {matchJson.Harmony}";
            newMatchCell.transform.GetChild(8).GetChild(5).gameObject.SetActive(matchJson.Coopertition);
            newMatchCell.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = matchJson.Comments;
            newMatchCell = Instantiate(newMatchCell, transform.parent);




        }
    }
}
