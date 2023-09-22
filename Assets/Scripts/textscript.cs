using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class textscript : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private Vector3 originalScale;
    public float scaleMultiplier = 1.2f; // Wspó³czynnik skali po najechaniu myszk¹

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        originalScale = textMeshProUGUI.transform.localScale;
    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {

    }
}
