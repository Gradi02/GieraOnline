using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public GameObject artefacts;

    public void Upgrade(string name)
    {
        if (artefacts.transform.Find(name).GetComponent<ArtefactManager>().GetLevel() == 0)
            artefacts.transform.Find(name).GetComponent<ArtefactManager>().Unlock();
        else
            artefacts.transform.Find(name).GetComponent<ArtefactManager>().Upgrade();
    }
}
