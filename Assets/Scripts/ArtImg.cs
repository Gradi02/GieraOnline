using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtImg : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titlename;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI unlock;

    private RawImage sr;
    private Artefacts manager;
    private bool locked = true;
    private Color newCol = Color.yellow;

    [HideInInspector] public ArtefactManager artefact;



    private float alphaValue;
    private bool increasingAlpha = true;

    private float minAlpha = 0.5f;
    private float maxAlpha = 1f;
    private float miganieSpeed = 0.3f;

    private void Awake()
    {
        sr = GetComponent<RawImage>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Artefacts>();
        //artefact = manager.RandomArt();
    }

    public void SetArtefact(ArtefactManager art_in)
    {
        artefact = art_in;

        if (artefact == null)
        {
            Destroy(gameObject);
        }
        else
        {
            titlename.text = artefact.art_name;
            img.sprite = artefact.art_icon;
            description.text = artefact.art_description;
            level.text = "level " + artefact.GetLevel();

            if (artefact.isLocked())
            {
                unlock.text = "Unlock";
                locked = true;
            }
            else
            {
                unlock.text = "Level up";
                locked = false;
            }   
        }        
    }

    public void UnlockUpgrade()
    {
        if (locked)
        {
            artefact.Unlock();
        }
        else
        {
            artefact.Upgrade();
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("artimg"))
        {
            Destroy(g);
        }

        manager.RefreshList();
        manager.GoNext();
    }

    private void FixedUpdate()
    {
        if(locked)
        {
            if (increasingAlpha)
            {
                alphaValue += miganieSpeed * Time.fixedDeltaTime;
                if (alphaValue >= maxAlpha)
                {
                    alphaValue = maxAlpha;
                    increasingAlpha = false;
                }
            }
            else
            {
                alphaValue -= miganieSpeed * Time.fixedDeltaTime;
                if (alphaValue <= minAlpha)
                {
                    alphaValue = minAlpha;
                    increasingAlpha = true;
                }
            }

            // Ustaw nowy kolor z aktualn¹ wartoœci¹ alphy
            Color newColor = new Color(newCol.r, newCol.g, newCol.b, alphaValue);
            sr.color = newColor;
        }
    }
}
