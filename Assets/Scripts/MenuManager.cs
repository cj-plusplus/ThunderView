using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TeamView()
    {
        SceneManager.LoadScene("TeamView");
    }
    public void Rankings()
    {
        SceneManager.LoadScene("Rankings");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void AllianceView()
    {
        SceneManager.LoadScene("AllianceView");
    }
}
