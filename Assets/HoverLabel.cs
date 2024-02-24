using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float tick = 0;
    public float hoverTime;
    bool timerOn = false;
    public string text;
    Transform label;
    Transform textTransform;
    float frameCounter = 0;
    // Start is called before the first frame update
    void Awake()
    {
        label = gameObject.transform.GetChild(0);
        textTransform = gameObject.transform.GetChild(0).GetChild(0);

        textTransform.GetComponent<Text>().text = text;
        setComponent(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCounter == 0) {
            setComponent(false);
            frameCounter++;
        }

        if (timerOn) {
            tick += Time.deltaTime;
            if (tick >= hoverTime) {
                setComponent(true);
                timerOn = false;
                tick = 0;
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        timerOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        setComponent(false);
        timerOn = false;
        tick = 0;
    }

    void setComponent(bool b)
    {
        label.gameObject.SetActive(b);
    }
}
