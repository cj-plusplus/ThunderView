using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDisplay : MonoBehaviour
{
    public float maxValue;
    public float value;
    Transform bar;
    Transform textTransform;
    // Start is called before the first frame update
    void Start()
    {
        bar = gameObject.transform.GetChild(0);
        textTransform = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.GetComponent<Text>().text = ((int)value).ToString();
        bar.GetComponent<RectTransform>().offsetMax = new Vector2(-gameObject.GetComponent<RectTransform>().sizeDelta.x * (maxValue - value) / maxValue, 0);
    }

    public void setValue(float v) {
        value = v;
    }

    public void setColor(Color c) {
        gameObject.transform.GetChild(0).GetComponent<Image>().color = c;
        //gameObject.transform.GetChild(1).GetComponent<Text>().color = c; Text unreadable :(
    }
}
