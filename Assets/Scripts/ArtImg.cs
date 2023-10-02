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
    [SerializeField] private Image imgbg;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private RawImage levelbg;
    [SerializeField] private RawImage levelbg2;

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

            if (artefact.isLocked())
            {
                unlock.text = "Unlock";
                locked = true;
                imgbg.color = Color.yellow;
                sr.color = Color.yellow;
                levelbg.color = Color.yellow;
                levelbg2.color = Color.yellow;
            }
            else
            {
                unlock.text = "Level up";
                locked = false;
                imgbg.color = Color.gray;
                sr.color = Color.gray;
                levelbg.color = Color.gray;
                levelbg2.color = Color.gray;
            }
            
            description.text = artefact.art_description[artefact.GetLevel()];
            
            for (int i = 0; i < artefact.GetLevel(); i++)
            {
                stars[i].GetComponent<Image>().color = Color.white;
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
}
