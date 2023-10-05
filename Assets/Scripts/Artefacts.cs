using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefacts : MonoBehaviour
{
    [Header("Artefakty")]
    private List<GameObject> artefacts = new List<GameObject>();
    private List<GameObject> artefactsList = new List<GameObject>();

    [Header("UI")]
    public GameObject artUI;
    public GameObject artimg;
    public GameObject spawn;


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
    public void GoNext()
    {
        artUI.SetActive(false);
        GetComponent<waves>().SetUpUI();
    }
}
