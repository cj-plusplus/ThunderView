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
    public float height;
    public float padding;
    public float scale = 1f;
    private float DEFAULT_FONT_SIZE = 30;
    Transform label;
    Transform textTransform;
    // Start is called before the first frame update
    void Awake()
    {
        label = gameObject.transform.GetChild(0);
        textTransform = gameObject.transform.GetChild(1);

        textTransform.GetComponent<Text>().text = text;
        textTransform.GetComponent<Text>().fontSize = (int)(DEFAULT_FONT_SIZE * scale);
    }

    // Update is called once per frame
    void Update()
    {
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
        Color l = label.GetComponent<Image>().color;
        Color t = textTransform.GetComponent<Text>().color;
        label.GetComponent<Image>().color = new Color(l.r, l.g, l.b, b ? 1f : 1f/255f);
        textTransform.GetComponent<Text>().color = new Color(t.r, t.g, t.b, b ? 1f : 1f/255f);

        label.GetComponent<RectTransform>().sizeDelta =
            new Vector2(
                textTransform.gameObject.GetComponent<RectTransform>().sizeDelta.x + padding * scale * 2,
                height * scale);
    }
}