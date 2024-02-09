using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitySucks : MonoBehaviour
{
    private GameObject dataManagerObject;
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManagerObject = GameObject.Find("DataManager");
        dataManager = dataManagerObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    public void UpdateTeam()
    {
        dataManager.TeamViewSetup(transform.GetChild(1).GetComponent<Text>().text);
    }
}
