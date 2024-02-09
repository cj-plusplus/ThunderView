using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamName : MonoBehaviour
{
    private Text text;
    public GameObject DataManagerObject;
    private DataManager dataManager;
    // Start is called before the first frame update
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Rankings") { gameObject.SetActive(false); }
        UpdateText();
        dataManager.updateTeamView.AddListener(UpdateText);
    }
    void UpdateText()
    {
        text = GetComponent<Text>();
        dataManager = DataManagerObject.GetComponent<DataManager>();
        text.text = $"Team {dataManager.fileJson.team} - {dataManager.fileJson.name}";
        if (text.text != "Team  - ") { gameObject.SetActive(true); } // This fixes a random bug i had where the text would show up too early
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
