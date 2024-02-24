using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DirectorySetup : MonoBehaviour
{
    private string readMeText = "IMPORTANT!\nPlease put input files into a folder named after the event key.\nInside that folder, make two folders, titled \"obj\" and \"subj\".\nObjective/Robot scout files go in obj and Subjective/Alliance scout files go in subj.\nHappy Scouting!";
    // Start is called before the first frame update
    void Start()
    {
        if (!(Directory.Exists("C:\\ThunderView\\input")))
        {
            Directory.CreateDirectory("C:\\ThunderView\\input\\obj");
            Directory.CreateDirectory("C:\\ThunderView\\input\\subj");
            File.WriteAllText("C:\\ThunderView\\input\\readme.txt", readMeText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
