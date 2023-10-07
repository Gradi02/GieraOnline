using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class trans_start : MonoBehaviour
{
    private float duration = 0;
    public RawImage trans;
    private bool transition = true;

    public float cooldown = 0;

    private void Start()
    {
        cooldown += Time.time + 1f;
    }
    void Update()
    {
        if (cooldown <= Time.time) transition = true;


        if (transition)
        {
            duration += Time.deltaTime;
            RawImage rawImage = trans.GetComponent<RawImage>();
            Color currentColor = rawImage.color;
            float alphaValue = Mathf.Lerp(1, 0, 0.1f * duration * 3);
            currentColor.a = alphaValue;
            rawImage.color = currentColor;
            if (alphaValue == 0) trans.gameObject.SetActive(false);
        }
        
    }
}
