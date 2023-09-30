using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefacts : MonoBehaviour
{
    public GameObject[] artefacts;

    private int num = 0;
    void Start()
    {
        num = artefacts.Length;
    }

    [ContextMenu("rand")]
    public void RandomArt()
    {
        int rand = Random.Range(0, num);
        artefacts[rand].GetComponent<ArtefactManager>().Unlock();
    }
}
