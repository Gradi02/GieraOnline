using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textmesh;
    void Start()
    {
        textmesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textmesh.SetText(damageAmount.ToString());
    }
}
