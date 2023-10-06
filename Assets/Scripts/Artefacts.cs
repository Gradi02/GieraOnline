using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Artefacts : MonoBehaviour
{
    [Header("Artefakty")]
    private List<GameObject> artefacts = new List<GameObject>();
    private List<GameObject> artefactsList = new List<GameObject>();
    private List<GameObject> myArtefacts = new List<GameObject>();

    [Header("UI")]
    public GameObject artUI;
    public GameObject artimg;
    public GameObject spawn;

    [Header("artmini")]
    public GameObject artmini;
    public GameObject artparent;
    public GameObject nothing;
    public Color maxColor;

    void Start()
    {
        foreach(Transform a in GameObject.FindGameObjectWithTag("Player").transform.Find("Artefacts").GetComponentInChildren<Transform>())
        {
            artefacts.Add(a.gameObject);
        }

        RefreshList();
        artUI.SetActive(false);
        
        foreach(GameObject art in artefacts)
        {
            art.SetActive(false);
        }

        myArtefacts.Clear();

        if (myArtefacts.Count == 0) nothing.SetActive(true);
        else nothing.SetActive(false);
    }

    public void RefreshList()
    {
        artefactsList.Clear();

        foreach(GameObject a in artefacts)
        {
            if(!a.GetComponent<ArtefactManager>().isMaxed())
            {
                artefactsList.Add(a);
            }
        }
    }

    public ArtefactManager RandomArt()
    {
        int rand = 0;
        if (artefactsList.Count > 0)
        {
            rand = Random.Range(0, artefactsList.Count);
            return artefactsList[rand].GetComponent<ArtefactManager>();
        }
        else
        {
            return null;
        }
    }

    public void SpawnImg()
    {
        artUI.SetActive(true);
        if(artefactsList.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (artefactsList.Count > 0)
                {
                    GameObject ai = Instantiate(artimg, spawn.transform.position, Quaternion.identity, spawn.transform);
                    ArtefactManager art = RandomArt();
                    ai.GetComponent<ArtImg>().SetArtefact(art);
                    artefactsList.Remove(art.gameObject);
                }
            }
        }
        else
        {
            GoNext();
        }
    }

    public void AddArt(ArtefactManager artefact_in)
    {
        foreach(GameObject a in myArtefacts)
        {
            if(a == artefact_in.gameObject)
            {
                return;
            }
        }

        myArtefacts.Add(artefact_in.gameObject);
    }
    public void GoNext()
    {
        artUI.SetActive(false);

        foreach(Transform g in artparent.transform.GetComponentInChildren<Transform>())
        {
            Destroy(g.gameObject);
        }

        for(int i=0; i<myArtefacts.Count; i++)
        {
            GameObject newArt = Instantiate(artmini, artparent.transform.position, Quaternion.identity, artparent.transform);
            
            newArt.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetComponent<Image>().sprite 
                = myArtefacts[i].GetComponent<ArtefactManager>().art_icon;
            
            newArt.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = myArtefacts[i].GetComponent<ArtefactManager>().art_name;
            newArt.transform.GetChild(0).transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = "level " + myArtefacts[i].GetComponent<ArtefactManager>().GetLevel();
            newArt.transform.GetChild(0).transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = myArtefacts[i].GetComponent<ArtefactManager>().art_description;

            if(myArtefacts[i].GetComponent<ArtefactManager>().GetLevel() >= 5)
            {
                newArt.GetComponent<Image>().color = maxColor;
                newArt.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = maxColor;
                newArt.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = maxColor;
                newArt.transform.GetChild(0).transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().color = maxColor;
                newArt.transform.GetChild(0).transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().color = maxColor;
            }
        }

        if (myArtefacts.Count == 0) nothing.SetActive(true);
        else nothing.SetActive(false);

        GetComponent<waves>().SetUpUI();
    }
}
