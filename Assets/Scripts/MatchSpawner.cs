using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchSpawner : MonoBehaviour
{
    public GameObject matchPrefab;
    public GameObject allianceMatchPrefab;
    public GameObject DataManagerObject;
    private DataManager dataManager;
    public GameObject matchCount;
    private int teamCount;
    private int allianceCount;
    // Start is called before the first frame update
    private void Start()
    {
        dataManager = DataManagerObject.GetComponent<DataManager>();
        if (SceneManager.GetActiveScene().name == "TeamView")
        {
            SpawnMatches();
        }
            dataManager.updateTeamView.AddListener(SpawnMatches);
    }

    void SpawnMatches()
    {

        teamCount = 0;
        allianceCount = 0;
        if (transform.parent.childCount > 0)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (i == 0) { continue; }
                Destroy(transform.parent.GetChild(i).gameObject);
            }
        }

        DataManager.Match[] matches = dataManager.fileJson.matches;

        foreach (var match in matches)

        {
            GameObject newMatchCell = matchPrefab;
            switch (match.AllianceColor)
            {
                case "Red":
                    newMatchCell.gameObject.GetComponent<RawImage>().color = new Color(1.0f, 0.35686274509f, 0.27058823529f, 1.0f); break;
                case "Blue":
                    newMatchCell.gameObject.GetComponent<RawImage>().color = new Color(0.490196f, 0.8274509f, 1.0f, 1.0f); break;
            }
            newMatchCell.transform.GetChild(0).GetComponent<Text>().text = match.MatchNumber.ToString(); // MatchNumberText
            newMatchCell.transform.GetChild(1).GetComponent<Text>().text = match.MatchType; // MatchTypeText
            newMatchCell.transform.GetChild(2).GetComponent<Text>().text = $"Data Quality: {match.DataQuality.ToString()}/5"; //DataQualityText
            newMatchCell.transform.GetChild(3).GetComponent<Text>().text = $"Scouter Name {match.ScouterName}"; //ScouterNameText
            newMatchCell.transform.GetChild(4).gameObject.SetActive(match.Replay); // Replay
            newMatchCell.transform.GetChild(5).GetComponent<Text>().text = $"DS {match.DriverStation.ToString()}"; // DriverStation
            newMatchCell.transform.GetChild(6).GetChild(1).GetComponent<Text>().text = match.AutoSpeaker.ToString(); // AutoSpeakerText
            newMatchCell.transform.GetChild(6).GetChild(3).GetComponent<Text>().text = match.AutoAmp.ToString(); // AutoAmpText
            newMatchCell.transform.GetChild(6).GetChild(5).GetComponent<Text>().text = match.AutoPickUpCenter.ToString(); // Pickuptext
            newMatchCell.transform.GetChild(6).GetChild(6).gameObject.SetActive(match.AStop); // AStop
            newMatchCell.transform.GetChild(6).GetChild(7).gameObject.SetActive(match.LeftWing); // LeftWing
            newMatchCell.transform.GetChild(7).GetChild(1).GetComponent<Text>().text = match.SpeakerNotesUnamped.ToString(); // SpeakerNotesUnamped
            newMatchCell.transform.GetChild(7).GetChild(3).GetComponent<Text>().text = match.SpeakerNotesAmped.ToString(); // SpeakerNotesAmped
            newMatchCell.transform.GetChild(7).GetChild(5).GetComponent<Text>().text = match.AmpNotes.ToString(); // SpeakerNotesUnamped'
            newMatchCell.transform.GetChild(7).GetChild(7).GetComponent<Text>().text = $"Source: {match.PickUpSource}\nGround: {match.PickUpGround}";
            newMatchCell.transform.GetChild(7).GetChild(8).gameObject.SetActive(match.Feeder);
            newMatchCell.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = match.EndLocation;
            newMatchCell.transform.GetChild(8).GetChild(1).gameObject.SetActive(match.Trap);
            newMatchCell.transform.GetChild(8).GetChild(2).gameObject.SetActive(match.Spotlight);
            if (SceneManager.GetActiveScene().name == "Rankings")
            {
                newMatchCell.transform.localScale = new Vector3(0.58f, 0.58f, 0.58f);
            } else
            {
                newMatchCell.transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
            }
            teamCount++;


            newMatchCell = Instantiate(newMatchCell, transform.parent);

        }

        foreach (var match in Directory.GetFiles(Application.persistentDataPath + "/2024cmptx/alliance") )
        {
            DataManager.AllianceMatch allianceFileJson = JsonUtility.FromJson<DataManager.AllianceMatch>(File.ReadAllText(match));
            if (allianceFileJson.Team1.ToString() == dataManager.fileJson.team || allianceFileJson.Team2.ToString() == dataManager.fileJson.team || allianceFileJson.Team3.ToString() == dataManager.fileJson.team) {

                GameObject newAllianceMatchCell = allianceMatchPrefab;
                newAllianceMatchCell.transform.GetChild(0).GetComponent<Text>().text = allianceFileJson.MatchNumber.ToString();
                newAllianceMatchCell.transform.GetChild(1).GetComponent<Text>().text = allianceFileJson.MatchType;
                newAllianceMatchCell.transform.GetChild(2).GetComponent<Text>().text = $"Data Quality: {allianceFileJson.DataQuality.ToString()}/5";
                newAllianceMatchCell.transform.GetChild(3).GetComponent<Text>().text = $"Scouter Name: {allianceFileJson.ScouterName}";
                newAllianceMatchCell.transform.GetChild(4).gameObject.SetActive(allianceFileJson.Replay);
                newAllianceMatchCell.transform.GetChild(5).GetComponent<Text>().text = allianceFileJson.Team1.ToString();
                newAllianceMatchCell.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = $"Defense: {allianceFileJson.Team1Defense}/5\nDriver Skill: {allianceFileJson.Team1DriverSkill}/5";
                newAllianceMatchCell.transform.GetChild(6).GetComponent<Text>().text = allianceFileJson.Team2.ToString();
                newAllianceMatchCell.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = $"Defense: {allianceFileJson.Team2Defense}/5\nDriver Skill: {allianceFileJson.Team2DriverSkill}/5";
                newAllianceMatchCell.transform.GetChild(7).GetComponent<Text>().text = allianceFileJson.Team3.ToString();
                newAllianceMatchCell.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = $"Defense: {allianceFileJson.Team3Defense}/5\nDriver Skill: {allianceFileJson.Team3DriverSkill}/5";
                newAllianceMatchCell.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = $"Auto Center Notes: {allianceFileJson.AutoCenterNotes}";
                newAllianceMatchCell.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = $"High Notes: {allianceFileJson.HighNotes}/{allianceFileJson.HighNotePotential}";
                newAllianceMatchCell.transform.GetChild(8).GetChild(2).GetComponent<Text>().text = $"Amplify Count: {allianceFileJson.AmplifyCount}";
                newAllianceMatchCell.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = $"Team At Amp: {allianceFileJson.TeamAtAmp}";
                newAllianceMatchCell.transform.GetChild(8).GetChild(4).GetComponent<Text>().text = $"Bots in Harmony: {allianceFileJson.Harmony}";
                newAllianceMatchCell.transform.GetChild(8).GetChild(5).gameObject.SetActive(allianceFileJson.Coopertition);
                newAllianceMatchCell.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = allianceFileJson.Comments;
                newAllianceMatchCell.transform.localScale = new Vector3(0.77f, 0.77f, 0.77f);
                newAllianceMatchCell = Instantiate(newAllianceMatchCell, transform.parent);
                allianceCount++;
            }
            
        }
        matchCount.GetComponent<Text>().text = $"Scouted Team Matches: {teamCount}\nScouted Alliance Matches: {allianceCount}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
