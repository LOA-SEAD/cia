using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private static TooltipSystem current;
    public GameObject tooltip; 

    void Start()
    {
        current = this;
    }

    public void Show()
    {
        StartCoroutine("StartDelay");

    }

    public void Hide()
    {
        StopCoroutine("StartDelay");
        current.tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = Input.mousePosition;
        tooltip.transform.position = position;
    }

    public IEnumerator StartDelay()
    {

        yield return new WaitForSeconds(0.5f);
        current.tooltip.SetActive(true);
    }
}
