using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        filepath = Application.persistentDataPath + $"/{PlayerPrefs.GetString("EventKey")}/subj";
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

            for (int i = 1; i < transform.parent.childCount; i++)
            {
                Destroy(transform.parent.GetChild(i).gameObject);
            }
        }
        if (!Directory.Exists(filepath)) { return; }
        foreach (var match in Directory.GetFiles(filepath))
        {
            DataManager.AllianceMatch matchJson = JsonUtility.FromJson<DataManager.AllianceMatch>(File.ReadAllText(match));
            if (selective != "") // Search function in alliance view
            {
                if (!(matchJson.Team1.ToString() == selective || matchJson.Team2.ToString() == selective || matchJson.Team3.ToString() == selective)) { continue; }
            }
            GameObject newMatchCell = matchPrefab;

            /* Background Color for Alliance MatchCell*/
            switch (matchJson.AllianceColor)
            {
                case "Red":
                    newMatchCell.gameObject.GetComponent<RawImage>().color = new Color(0.9176471f, 0.2784313f, 0.3095479f, 1.0f); break;
                case "Blue":
                    newMatchCell.gameObject.GetComponent<RawImage>().color = new Color(0.2800916f, 0.3204849f, 0.9182389f, 1.0f); break;
            }

            newMatchCell.transform.GetChild(0).GetComponent<Text>().text = matchJson.MatchNumber.ToString();
            newMatchCell.transform.GetChild(1).GetComponent<Text>().text = matchJson.MatchType;
            newMatchCell.transform.GetChild(2).GetComponent<Text>().text = $"Data Quality: {matchJson.DataQuality.ToString()}/5";
            newMatchCell.transform.GetChild(3).GetComponent<Text>().text = $"Scouter Name: {matchJson.ScouterName}";
            newMatchCell.transform.GetChild(4).gameObject.SetActive(matchJson.Replay);

            // Team 1 Stats
            newMatchCell.transform.GetChild(5).GetComponent<Text>().text = matchJson.Team1.ToString();
            newMatchCell.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = $"Avoidance Score {matchJson.Team1Avoid}";
            newMatchCell.transform.GetChild(5).GetChild(0).GetComponent<Text>().color = matchJson.Team1Avoid == 3 ? Color.green : matchJson.Team1Avoid == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = $"Travel Speed Score {matchJson.Team1TravelSpeed}";
            newMatchCell.transform.GetChild(5).GetChild(1).GetComponent<Text>().color = matchJson.Team1TravelSpeed == 3 ? Color.green : matchJson.Team1TravelSpeed == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(5).GetChild(2).GetComponent<Text>().text = $"Align Speed Score {matchJson.Team1AlignSpeed}";
            newMatchCell.transform.GetChild(5).GetChild(2).GetComponent<Text>().color = matchJson.Team1AlignSpeed == 3 ? Color.green : matchJson.Team1AlignSpeed == 2 ? Color.white : Color.black;

            // Team 2 Stats
            newMatchCell.transform.GetChild(6).GetComponent<Text>().text = matchJson.Team2.ToString();
            newMatchCell.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = $"Avoidance Score {matchJson.Team2Avoid}";
            newMatchCell.transform.GetChild(6).GetChild(0).GetComponent<Text>().color = matchJson.Team2Avoid == 3 ? Color.green : matchJson.Team2Avoid == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(6).GetChild(1).GetComponent<Text>().text = $"Travel Speed Score {matchJson.Team2TravelSpeed}";
            newMatchCell.transform.GetChild(6).GetChild(1).GetComponent<Text>().color = matchJson.Team2TravelSpeed == 3 ? Color.green : matchJson.Team2TravelSpeed == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = $"Align Speed Score {matchJson.Team2AlignSpeed}";
            newMatchCell.transform.GetChild(6).GetChild(2).GetComponent<Text>().color = matchJson.Team2AlignSpeed == 3 ? Color.green : matchJson.Team2AlignSpeed == 2 ? Color.white : Color.black;

            // Team 3 Stats
            newMatchCell.transform.GetChild(7).GetComponent<Text>().text = matchJson.Team3.ToString();
            newMatchCell.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = $"Avoidance Score {matchJson.Team3Avoid}";
            newMatchCell.transform.GetChild(7).GetChild(0).GetComponent<Text>().color = matchJson.Team3Avoid == 3 ? Color.green : matchJson.Team3Avoid == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(7).GetChild(1).GetComponent<Text>().text = $"Travel Speed Score {matchJson.Team3TravelSpeed}";
            newMatchCell.transform.GetChild(7).GetChild(1).GetComponent<Text>().color = matchJson.Team3TravelSpeed == 3 ? Color.green : matchJson.Team3TravelSpeed == 2 ? Color.white : Color.black;
            newMatchCell.transform.GetChild(7).GetChild(2).GetComponent<Text>().text = $"Align Speed Score {matchJson.Team3AlignSpeed}";
            newMatchCell.transform.GetChild(7).GetChild(2).GetComponent<Text>().color = matchJson.Team3AlignSpeed == 3 ? Color.green : matchJson.Team3AlignSpeed == 2 ? Color.white : Color.black;

            newMatchCell.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = $"Auto Center Notes: {matchJson.AutoCenterNotes}";
            newMatchCell.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = $"High Notes: {matchJson.HighNotes}/{matchJson.HighNotePotential}";
            newMatchCell.transform.GetChild(8).GetChild(2).GetComponent<Text>().text = $"Amplify Count: {matchJson.AmplifyCount}";
            newMatchCell.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = $"Team At Amp: {matchJson.TeamAtAmp}";
            newMatchCell.transform.GetChild(8).GetChild(4).GetComponent<Text>().text = $"Bots in Harmony: {matchJson.Harmony}";
            newMatchCell.transform.GetChild(8).GetChild(5).gameObject.SetActive(matchJson.Coopertition);
            newMatchCell.transform.GetChild(8).GetChild(6).GetComponent<Text>().text = $"Fouls: {matchJson.Fouls}";
            newMatchCell.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = $"Ranking Explanation: {matchJson.RankingComments}\nGeneral Strategy: {matchJson.StratComments}\nOther Comments:{matchJson.OtherComments}";
            newMatchCell.transform.GetChild(10).GetComponent<Text>().text = matchJson.WinMatch ? "WIN" : "LOSS";

            if (SceneManager.GetActiveScene().name == "Rankings")
            {
                newMatchCell.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            }
            else
            {
                newMatchCell.transform.localScale = new Vector3(0.77f, 0.77f, 0.77f);
            }

            newMatchCell = Instantiate(newMatchCell, transform.parent);

        }
    }
}
