using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    // Update is called once per frame
    void Update()
    {
        //Highlight
        if(highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if(highlight.CompareTag("Selectable") && highlight != selection)
            {
                if(highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.AddComponent<Outline>().OutlineColor = Color.yellow;
                    highlight.gameObject.AddComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }
        //Selection
        if(Input.GetMouseButtonDown(0))
        {
            if(highlight)
            {
                if(selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.AddComponent<Outline>().enabled = true;
                highlight = null;
            }
            else
            {
                if(selection)
                {
                    selection.gameObject.AddComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }
}
