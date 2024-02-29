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
            DataManager.AllianceMatch allianceFileJson = JsonUtility.FromJson<DataManager.AllianceMatch>(File.ReadAllText(match));
            if (selective != "") // Search function in alliance view
            {
                if (!(allianceFileJson.Team1.ToString() == selective || allianceFileJson.Team2.ToString() == selective || allianceFileJson.Team3.ToString() == selective)) { continue; }
            }
            GameObject newAllianceMatchCell = matchPrefab;

            /* Background Color for Alliance MatchCell*/
            switch (allianceFileJson.AllianceColor)
            {
                case "Red":
                    newAllianceMatchCell.gameObject.GetComponent<RawImage>().color = new Color(0.9176471f, 0.2784313f, 0.3095479f, 1.0f); break;
                case "Blue":
                    newAllianceMatchCell.gameObject.GetComponent<RawImage>().color = new Color(0.2800916f, 0.3204849f, 0.9182389f, 1.0f); break;
            }

            newAllianceMatchCell.transform.GetChild(0).GetComponent<Text>().text = allianceFileJson.MatchNumber.ToString();
            newAllianceMatchCell.transform.GetChild(1).GetComponent<Text>().text = allianceFileJson.MatchType;
            newAllianceMatchCell.transform.GetChild(2).GetComponent<Text>().text = $"Data Quality: {allianceFileJson.DataQuality.ToString()}/5";
            newAllianceMatchCell.transform.GetChild(3).GetComponent<Text>().text = $"Scouter Name: {allianceFileJson.ScouterName}";
            newAllianceMatchCell.transform.GetChild(4).gameObject.SetActive(allianceFileJson.Replay);

            // Team 1 Stats
            newAllianceMatchCell.transform.GetChild(5).GetComponent<Text>().text = allianceFileJson.Team1.ToString();
            newAllianceMatchCell.transform.GetChild(5).GetChild(0).GetComponent<BarDisplay>().setValue(allianceFileJson.Team1Avoid);
            newAllianceMatchCell.transform.GetChild(5).GetChild(0).GetComponent<BarDisplay>().setColor(allianceFileJson.Team1Avoid == 3 ? Color.green : allianceFileJson.Team1Avoid == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(5).GetChild(1).GetComponent<BarDisplay>().setValue(allianceFileJson.Team1TravelSpeed);
            newAllianceMatchCell.transform.GetChild(5).GetChild(1).GetComponent<BarDisplay>().setColor(allianceFileJson.Team1TravelSpeed == 3 ? Color.green : allianceFileJson.Team1TravelSpeed == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(5).GetChild(2).GetComponent<BarDisplay>().setValue(allianceFileJson.Team1AlignSpeed);
            newAllianceMatchCell.transform.GetChild(5).GetChild(2).GetComponent<BarDisplay>().setColor(allianceFileJson.Team1AlignSpeed == 3 ? Color.green : allianceFileJson.Team1AlignSpeed == 2 ? Color.white : Color.black);

            // Team 2 Stats
            newAllianceMatchCell.transform.GetChild(6).GetComponent<Text>().text = allianceFileJson.Team1.ToString();
            newAllianceMatchCell.transform.GetChild(6).GetChild(0).GetComponent<BarDisplay>().setValue(allianceFileJson.Team2Avoid);
            newAllianceMatchCell.transform.GetChild(6).GetChild(0).GetComponent<BarDisplay>().setColor(allianceFileJson.Team2Avoid == 3 ? Color.green : allianceFileJson.Team1Avoid == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(6).GetChild(1).GetComponent<BarDisplay>().setValue(allianceFileJson.Team2TravelSpeed);
            newAllianceMatchCell.transform.GetChild(6).GetChild(1).GetComponent<BarDisplay>().setColor(allianceFileJson.Team2TravelSpeed == 3 ? Color.green : allianceFileJson.Team1TravelSpeed == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(6).GetChild(2).GetComponent<BarDisplay>().setValue(allianceFileJson.Team2AlignSpeed);
            newAllianceMatchCell.transform.GetChild(6).GetChild(2).GetComponent<BarDisplay>().setColor(allianceFileJson.Team2AlignSpeed == 3 ? Color.green : allianceFileJson.Team1AlignSpeed == 2 ? Color.white : Color.black);

            // Team 3 Stats
            newAllianceMatchCell.transform.GetChild(7).GetComponent<Text>().text = allianceFileJson.Team1.ToString();
            newAllianceMatchCell.transform.GetChild(7).GetChild(0).GetComponent<BarDisplay>().setValue(allianceFileJson.Team3Avoid);
            newAllianceMatchCell.transform.GetChild(7).GetChild(0).GetComponent<BarDisplay>().setColor(allianceFileJson.Team3Avoid == 3 ? Color.green : allianceFileJson.Team1Avoid == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(7).GetChild(1).GetComponent<BarDisplay>().setValue(allianceFileJson.Team3TravelSpeed);
            newAllianceMatchCell.transform.GetChild(7).GetChild(1).GetComponent<BarDisplay>().setColor(allianceFileJson.Team3TravelSpeed == 3 ? Color.green : allianceFileJson.Team1TravelSpeed == 2 ? Color.white : Color.black);
            newAllianceMatchCell.transform.GetChild(7).GetChild(2).GetComponent<BarDisplay>().setValue(allianceFileJson.Team3AlignSpeed);
            newAllianceMatchCell.transform.GetChild(7).GetChild(2).GetComponent<BarDisplay>().setColor(allianceFileJson.Team3AlignSpeed == 3 ? Color.green : allianceFileJson.Team1AlignSpeed == 2 ? Color.white : Color.black);

            newAllianceMatchCell.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = $"Auto Center Notes: {allianceFileJson.AutoCenterNotes}";
            newAllianceMatchCell.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = $"High Notes: {allianceFileJson.HighNotes}/{allianceFileJson.HighNotePotential}";
            newAllianceMatchCell.transform.GetChild(8).GetChild(2).GetComponent<Text>().text = $"Amplify Count: {allianceFileJson.AmplifyCount}";
            newAllianceMatchCell.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = $"Team At Amp: {allianceFileJson.TeamAtAmp}";
            newAllianceMatchCell.transform.GetChild(8).GetChild(4).GetComponent<Text>().text = $"Bots in Harmony: {allianceFileJson.Harmony}";
            newAllianceMatchCell.transform.GetChild(8).GetChild(5).gameObject.SetActive(allianceFileJson.Coopertition);
            newAllianceMatchCell.transform.GetChild(8).GetChild(6).GetComponent<Text>().text = $"Fouls: {allianceFileJson.Fouls}";
            newAllianceMatchCell.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = $"Ranking Explanation: {allianceFileJson.RankingComments}\nGeneral Strategy: {allianceFileJson.StratComments}\nOther Comments:{allianceFileJson.OtherComments}";
            newAllianceMatchCell.transform.GetChild(10).GetComponent<Text>().text = allianceFileJson.WinMatch ? "WIN" : "LOSS";

            if (SceneManager.GetActiveScene().name == "Rankings")
            {
                newAllianceMatchCell.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            }
            else
            {
                newAllianceMatchCell.transform.localScale = new Vector3(0.77f, 0.77f, 0.77f);
            }

            newAllianceMatchCell = Instantiate(newAllianceMatchCell, transform.parent);

        }
    }
}
