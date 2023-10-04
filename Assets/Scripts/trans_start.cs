using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class trans_start : MonoBehaviour
{
    private float duration = 0;
    public RawImage trans;
    private bool transition = false;

    private void Start()
    {
        transition = true;
    }
    void Update()
    {
        if (transition)
        {
            duration += Time.deltaTime;
            RawImage rawImage = trans.GetComponent<RawImage>();
            Color currentColor = rawImage.color;
            float alphaValue = Mathf.Lerp(1, 0, 0.1f * duration * 5);
            currentColor.a = alphaValue;
            rawImage.color = currentColor;
            //if (alphaValue == 0) trans.gameObject.SetActive(false);
        }
    }
}
