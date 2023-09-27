using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgPopup : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private float startFontSize = 20f;
    private float endFontSize = 0f;
    private float duration = 1f;
    private bool wait = true;

    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator ChangeFontSizeOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newSize = Mathf.Lerp(startFontSize, endFontSize, elapsedTime / duration);
            textMeshPro.fontSize = newSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f); // 
        wait = false;

        if (!wait)
        {
            StartCoroutine(ChangeFontSizeOverTime());
        }
    }
}