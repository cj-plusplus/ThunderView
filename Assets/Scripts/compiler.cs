using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class compiler : MonoBehaviour
{
    public string filepath = "C:\\ThunderView\\input";
    public string outputPath;
    public string eventKey;
    // Start is called before the first frame update
    void Start()
    {
        outputPath = Application.persistentDataPath;
    }

    // Update is called once per frame

    public void updateEventKey()
    {
        PlayerPrefs.SetString("EventKey", GameObject.Find("EventKey").GetComponent<TMP_InputField>().text);
    }

    public void ObjCompile()
    {
        eventKey = PlayerPrefs.GetString("EventKey");
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

            } else
            {
                teamFile = new TeamFile();
                teamFile.team = matchJson.TeamNumber.ToString();
                teamFile.name = "IDK";
                matchList = new List<Match>();

            }
           
            matchList.Add(matchJson);
            teamFile.matches = matchList.ToArray();
            File.WriteAllText(teamFilePath, JsonUtility.ToJson(teamFile));

        }
    
    }
    public void SubjCompile()
    {
        eventKey = PlayerPrefs.GetString("EventKey");
        foreach (var match in Directory.GetFiles(filepath + "/subj"))
        {
            File.WriteAllText(outputPath + "/" + eventKey + "/subj",File.ReadAllText(match));
        }
    }

        [System.Serializable]
    public class TeamFile
    {
        public string team;
        public string name;
        public Match[] matches;
    }

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
        public int Team1Defense;
        public int Team2Defense;
        public int Team3Defense;
        public int Team1DriverSkill;
        public int Team2DriverSkill;
        public int Team3DriverSkill;
        public int AmplifyCount;
        public bool Coopertition;
        public int HighNotes;
        public int HighNotePotential;
        public int Harmony;
        public string Comments;

    }
    [System.Serializable]
    public class Match
    {
        public int TeamNumber;
        public string MatchType;
        public int MatchNumber;
        public int DataQuality;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
        public bool LeftWing;
        public int AutoSpeaker;
        public int AutoAmp;
        public int AutoPickUpCenter;
        public bool AStop;
        public int PickUpGround;
        public int PickUpSource;
        public int SpeakerNotesUnamped;
        public int SpeakerNotesAmped;
        public int AmpNotes;
        public bool Feeder;
        public string EndLocation;
        public bool Spotlight;
        public bool Trap;
        public string Comments;
    }
}
