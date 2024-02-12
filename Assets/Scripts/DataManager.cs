using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string eventKey;
    public string teamNumber;
    private string teamFile;
    public TeamFile fileJson;
    public UnityEvent updateTeamView;
    void Start()
    {
        eventKey = PlayerPrefs.GetString(eventKey);
        if (!Directory.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("EventKey") + "/obj")) { }
        //if (SceneManager.GetActiveScene().name == "TeamView") { TeamViewSetup("254"); } 


    }

    public void TeamViewSetup(string teamNumber)
    {

        teamFile = File.ReadAllText(Application.persistentDataPath + "/" + PlayerPrefs.GetString("EventKey") + "/obj/" + teamNumber + ".json");
        fileJson = JsonUtility.FromJson<TeamFile>(teamFile);
        updateTeamView.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
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
    [System.Serializable]
    public class TeamFile {
        public string team;
        public string name;
        public Match[] matches;
    }

  
    public string DataStats(string filepath, string key, string mode, bool boolMode = false)
    {
        string statTeamFile = File.ReadAllText(filepath);
        TeamFile statFileJson = JsonUtility.FromJson<TeamFile>(statTeamFile);

        List<int> dataSet = new List<int>();
        foreach (var match in statFileJson.matches)
        {
            string matchValue = match.GetType().GetField(key).GetValue(match).ToString();

            if (boolMode) { dataSet.Add(matchValue == "True" ? 1 : 0); }
            else
            {
                dataSet.Add(Int32.Parse(matchValue));
            }
        } // At this point we have a list with every data value as an integer, so we can now manipulate data

        switch (mode) { 
            case "Average":
                double average = (double)dataSet.Sum() / dataSet.Count;
                return average.ToString();
            case "Average (Deviation)":
                double average2 = dataSet.Average();
                double sum = dataSet.Sum(d => Math.Pow(d - average2,2));
                double deviation = Math.Sqrt((sum) / (dataSet.Count - 1));
                return $"{average2} ± {Math.Round(deviation,3)}";
            case "Minimum":
                return dataSet.Min().ToString();
            case "Maximum":
                return dataSet.Max().ToString();

    }
        return "error :(";
    }
    public string MiscStats(string filepath, string mode)
    {
        string miscTeamFile = File.ReadAllText(filepath);
        TeamFile miscFileJson = JsonUtility.FromJson<TeamFile>(miscTeamFile);
        switch (mode)
        {
            // These two really don't fit under the category of "stats", but whatever
            case "length":
                return miscFileJson.matches.Length.ToString();
            case "teamNum":
                return miscFileJson.team;

        }
        return "error :(";
    }
    }
