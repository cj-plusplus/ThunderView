using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Label : MonoBehaviour
{
    public GameObject label;
    public float timer = 0;
    public bool timerOn = false;
    Ray ray;
	RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        label.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn && timer <= 1) { timer += Time.deltaTime; }
        if (timer >= 1) { label.SetActive(true); timerOn = false; }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			Debug.Log("AFEWF");
		}
    }

    public void OnMouseEnter(PointerEventData eventData)
    {
        Debug.Log("HEY");
        timerOn = true;
        timer = 0;
    }

    public void OnMouseExit(PointerEventData eventData)
    {
        label.SetActive(false);
    }
}
