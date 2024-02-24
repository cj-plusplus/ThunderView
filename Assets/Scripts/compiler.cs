using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class compiler : MonoBehaviour
{
    public string filepath = "C:\\ThunderView\\input";
    public string outputPath;
    public string eventKey;
    public GameObject importResultText;
    // Start is called before the first frame update
    void Start()
    {
        outputPath = Application.persistentDataPath;
        GameObject.Find("EventKey").GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("EventKey");
    }

    // Update is called once per frame

    public void updateEventKey()
    {
        PlayerPrefs.SetString("EventKey", GameObject.Find("EventKey").GetComponent<TMP_InputField>().text);
    }

    public void ObjCompile()
    {
        int objCount = 0;
        int subjCount = 0;
        eventKey = PlayerPrefs.GetString("EventKey");
        if (!Directory.Exists(outputPath + "/" + eventKey)) { Debug.Log("Making directories"); Directory.CreateDirectory(outputPath + "/" + eventKey + "/obj"); Directory.CreateDirectory(outputPath + "/" + eventKey + "/subj"); }
        foreach (var match in Directory.GetFiles(filepath + "/obj"))
        {
            Match matchJson = JsonUtility.FromJson<Match>(File.ReadAllText(match));
            string teamFilePath = outputPath + "\\" + eventKey + "\\obj\\" + matchJson.TeamNumber + ".json";
            TeamFile teamFile;
            List<Match> matchList;
            if (File.Exists(teamFilePath))
            {
                teamFile = JsonUtility.FromJson<TeamFile>(File.ReadAllText(teamFilePath));
                matchList = teamFile.matches.ToList();

            } else // Creates a new Team File to store matches
            {
                teamFile = new TeamFile();
                teamFile.team = matchJson.TeamNumber.ToString();
                teamFile.name = "";
                matchList = new List<Match>();

            }
            bool equalChecker = false;
            foreach (Match existingMatch in matchList) // Nested for loops to check duplicate data
            {
                bool localEqualChecker = true;
                // Guilty unless proven innocent logic, if all fields are equal than this value will never be changed
                foreach (FieldInfo field in existingMatch.GetType().GetFields())
                {
                    
                    if (!(field.GetValue(existingMatch).ToString() == field.GetValue(matchJson).ToString())) { Debug.Log($"{field.Name} is not equal.\n Duplicate match: {field.GetValue(matchJson)}\nExisting Match: {field.GetValue(existingMatch)}"); localEqualChecker = false; }
                }
                if (localEqualChecker) { Debug.Log("FOUND DUPLICATE MATCH " + existingMatch.MatchNumber); equalChecker = true; }
                
            }
            if (!equalChecker)
            {
                matchList.Add(matchJson);
                objCount++;
                teamFile.matches = matchList.ToArray();
                File.WriteAllText(teamFilePath, JsonUtility.ToJson(teamFile));
            }
            //File.Delete(match);

        }
        foreach (var match in Directory.GetFiles(filepath + "/subj"))
        {
            AllianceMatch matchJson = JsonUtility.FromJson<AllianceMatch>(File.ReadAllText(match));
            bool equalChecker = false;
            foreach (var existingMatchName in Directory.GetFiles($"{outputPath}/{PlayerPrefs.GetString("EventKey")}/subj")) // Nested for loops to check duplicate data
            {
                AllianceMatch existingMatch = JsonUtility.FromJson<AllianceMatch>(File.ReadAllText(existingMatchName));
                bool localEqualChecker = true;
                // Guilty unless proven innocent logic, if all fields are equal than this value will never be changed
                foreach (FieldInfo field in existingMatch.GetType().GetFields())
                {

                    if (!(field.GetValue(existingMatch).ToString() == field.GetValue(matchJson).ToString())) { Debug.Log($"{field.Name} is not equal.\n Duplicate match: {field.GetValue(matchJson)}\nExisting Match: {field.GetValue(existingMatch)}"); localEqualChecker = false; }
                }
                if (localEqualChecker) { Debug.Log("FOUND DUPLICATE MATCH " + existingMatch.MatchNumber); equalChecker = true; }

            }
            if (!equalChecker)
            {
                string[] matchFileName = match.Split("/");
                File.WriteAllText($"{outputPath}/{eventKey}/{matchFileName[matchFileName.Length - 1]}" , File.ReadAllText(match));
                subjCount++;
            }
            //File.Delete(match);
        }
        importResultText.gameObject.SetActive(true);
        importResultText.GetComponent<Text>().text = $"Imported {objCount} objective files and {subjCount} subjective files.";
    }

        [System.Serializable]
    public class TeamFile
    {
        public string team;
        public string name;
        public Match[] matches;
    }

    [System.Serializable]
    public class Match
    {
        public int TeamNumber;
        public string MatchType;
        public int DataQuality;
        public int MatchNumber;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
        public bool Preload;
        public string StartPos;
        public bool LeftWing;
        public int AutoSpeaker;
        public int AutoAmp;
        public int AutoPickUpWing;
        public int AutoPickUpCenter;
        public bool AStop;
        public int PickUpGround;
        public int PickUpSource;
        public int SpeakerNotesUnamped;
        public int SpeakerNotesAmped;
        public int AmpNotes;
        public bool Feeder;
        public bool Coopertition;
        public bool Onstage;
        public bool Park;
        public bool Spotlight;
        public bool Trap;
        public string Comments;
    }
    [System.Serializable]
    public class AllianceMatch
    {
        public int MatchNumber;
        public string MatchType;
        public int DataQuality;
        public bool Replay;
        public string AllianceColor;
        public string ScouterName;
        public int Team1; // Anything after with the suffix "1" refers to robot 1
        public int Team2; // Anything after with the suffix "2" refers to robot 2
        public int Team3; // Anything after with the suffix "3" refers to robot 3
        public int TeamAtAmp;
        public int AutoCenterNotes;
        public int Team1TravelSpeed;
        public int Team2TravelSpeed;
        public int Team3TravelSpeed;
        public int Team1AlignSpeed;
        public int Team2AlignSpeed;
        public int Team3AlignSpeed;
        public int Team1Avoid;
        public int Team2Avoid;
        public int Team3Avoid;

        public int AmplifyCount;
        public int Fouls;
        public bool Coopertition;
        public int HighNotes;
        public int HighNotePotential;
        public string Harmony;
        public string RankingComments;
        public string StratComments;
        public string OtherComments;
        public bool WinMatch;

    }
}
