using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ClearInAppMatches : MonoBehaviour
{

    [SerializeField] GameObject resultText;
    public void clearThunderViewLocalLow()
    {
        EmptyDir(Application.persistentDataPath);

        resultText.SetActive(true);
        resultText.GetComponent<Text>().text = "Cleared data; you can import more data";
    }

    private void EmptyDir(string path)
    {
        System.IO.DirectoryInfo di = new DirectoryInfo(path);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}
