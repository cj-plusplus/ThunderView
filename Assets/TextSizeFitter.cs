using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSizeFitter : MonoBehaviour
{
    public float height;
    public float padding;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(
                gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x + padding * 2,
                height);
    }
}
