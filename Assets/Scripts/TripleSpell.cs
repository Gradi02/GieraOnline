using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleSpell : MonoBehaviour
{
    private Shooting shooting;
    private ArtefactManager art;

    private readonly int nextDouble = 13;

    private int nextTriple = 4;

    private void Start()
    {
        shooting = transform.root.GetComponent<Shooting>();
        art = GetComponent<ArtefactManager>();
    }
    void Update()
    {
        if(shooting.shots >= nextDouble - 2 * art.GetLevel())
        {
            shooting.shots = 0;

            if(nextTriple <= 0)
            {
                nextTriple = 4;

                shooting.single_shoot = false;
                shooting.double_shoot = false;
                shooting.triple_shoot = true;
            }
            else
            {
                nextTriple--;

                shooting.single_shoot = false;
                shooting.double_shoot = true;
                shooting.triple_shoot = false;
            }
        }
    }
}
