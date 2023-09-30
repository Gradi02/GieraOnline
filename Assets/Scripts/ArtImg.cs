using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtImg : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titlename;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI unlock;
    [SerializeField] private TextMeshProUGUI level;

    private RawImage sr;
    private Artefacts manager;
    private bool locked = true;

    [HideInInspector] public ArtefactManager artefact;

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
            sr.color = artefact.GetRarityColor();

            if (artefact.isLocked())
            {
                unlock.text = "Unlock";
                locked = true;
                level.gameObject.SetActive(false);
            }
            else
            {
                unlock.text = "Upgrade";
                locked = false;
                level.text = "level " + artefact.GetLevel().ToString() + "/5";
            }
        }
    }

    public void UnlockUpgrade()
    {
        if(locked)
        {
            artefact.Unlock();
        }
        else
        {
            artefact.Upgrade();
        }
        
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("artimg"))
        {
            Destroy(g);
        }

        manager.RefreshList();
        manager.GoNext();

    }
}
