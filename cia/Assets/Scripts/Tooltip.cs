using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    void Update()
    {
        int headerLength = header.text.Length;
        int contentLength = content.text.Length;
        Debug.Log("antes");
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

        Vector2 position = Input.mousePosition;
        transform.position = position;
    }
}
