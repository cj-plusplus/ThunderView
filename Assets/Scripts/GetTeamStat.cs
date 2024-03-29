using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetTeamStat : MonoBehaviour
{
    public GameObject dataManagerObject;
    private DataManager dataManager;
    private Dropdown statDropdown;
    private Text resultText;
    
    private string[] bools = { "Replay", "LeftWing", "AStop", "Feeder","Spotlight","Trap" };
    private string[] fields = { "DataQuality", "Replay", "LeftWing", "AutoAmp", "AutoSpeaker", "AutoPickUpCenter", "AStop", "PickUpGround", "PickUpSource", "SpeakerNotesUnamped", "SpeakerNotesAmped", "AmpNotes", "Feeder", "Spotlight", "Trap" };
    // Start is called before the first frame update
    void Start()
    {
        statDropdown = transform.GetChild(0).GetComponent<Dropdown>();
        resultText = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        dataManager = dataManagerObject.GetComponent<DataManager>();
        Get();
    }

    // Update is called once per frame
    public void Get()
    {
        string filepath = filepath = Application.persistentDataPath + $"/{PlayerPrefs.GetString("EventKey")}/obj/" + dataManager.fileJson.team + ".json";
        if (!File.Exists(filepath)) { statDropdown.gameObject.SetActive(false); resultText.text = ""; return; }
        statDropdown.gameObject.SetActive(true);
        string stat = statDropdown.options[statDropdown.value].text;
        if (stat == "None") { transform.GetChild(1).gameObject.SetActive(false); resultText.text = ""; return; }
        transform.GetChild(1).gameObject.SetActive(true);
        resultText.text = "";
        foreach (var key in fields)
        {
            if (bools.Any(key.Contains)) { resultText.text = resultText.text + key + " : " + dataManager.DataStats(filepath, key, stat, true) + "\n"; }
            else { resultText.text = resultText.text + key + " : " + dataManager.DataStats(filepath, key, stat) + "\n"; }
        }
        
    }
    public void Get(string teamNum)
    {
        string filepath = filepath = Application.persistentDataPath + $"/{PlayerPrefs.GetString("EventKey")}/obj/" + teamNum + ".json";
        if (!File.Exists(filepath)) { statDropdown.gameObject.SetActive(false); resultText.text = ""; return; }
        statDropdown.gameObject.SetActive(true);
        string stat = statDropdown.options[statDropdown.value].text;
        if (stat == "None") { transform.GetChild(1).gameObject.SetActive(false); resultText.text = ""; return; }
        transform.GetChild(1).gameObject.SetActive(true);
        resultText.text = "";
        foreach (var key in fields)
        {
            if (bools.Any(key.Contains)) { resultText.text = resultText.text + key + " : " + dataManager.DataStats(filepath, key, stat, true) + "\n"; }
            else { resultText.text = resultText.text + key + " : " + dataManager.DataStats(filepath, key, stat) + "\n"; }
        }

    }
}
