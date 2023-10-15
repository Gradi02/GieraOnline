using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletsManager : MonoBehaviour
{
    public float bulletSpeed = 15;
    public ParticleSystem bulletparticle;
    private GameObject player;
    private PlayerInfo info;

    public GameObject spark;
    private GameObject sparky;
    private GameObject chainArt;

    private bool chain = true;
    private bool book = false;
    void Start()
    {
        Destroy(this.gameObject, 10);
        player = GameObject.FindGameObjectWithTag("Player");

        ParticleSystem ps = bulletparticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule col = bulletparticle.main;
        //col.startColor = player.GetComponent<ChangeMode>().GetColor();
        info = player.GetComponent<PlayerInfo>();
        sparky = player.transform.GetChild(1).transform.Find("Sparky").gameObject;
        chainArt = player.transform.GetChild(1).transform.Find("Chain Bullet").gameObject;

        chain = player.GetComponent<Shooting>().GetChain();
        book = player.GetComponent<Shooting>().GetBook();
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
        //currentMode = player.GetComponent<ChangeMode>().GetMode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool crit = false;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageDelta = (int)Mathf.Round(info.GetDamage() * Random.Range(1.0f, (float)info.GetMultiplier()));
            if (Random.Range(0, 100) <= info.GetCritChance())
            {
                damageDelta = (int)Mathf.Round(damageDelta * info.GetCritMulti());
                crit = true;
            }

            if(book)
            {
                int rand = Random.Range(0, 100);

                if (rand <= 2 + player.transform.GetChild(1).transform.Find("Death Note").GetComponent<ArtefactManager>().GetLevel())
                {
                    collision.gameObject.GetComponent<EnemyInfo>().Damage(99999, false, Color.black);
                    Destroy(this.gameObject);
                    return;
                }
            }

            //ARTEFAKT SPARKY
            if (sparky.activeSelf)
            {
                int level = sparky.GetComponent<ArtefactManager>().GetLevel();
                int spark_quantity = Random.Range(level, 5 + level);

                for (int i = 0; i < spark_quantity; i++)
                {
                    Vector3 randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                    Quaternion randomQuaternion = Quaternion.Euler(randomRotation);
                    Instantiate(spark, transform.position, randomQuaternion);
                }
            }

            if (chain)
            {
                int rand = Random.Range(0, 4);

                if (rand == 0)
                {
                    int chainMax = 2;
                    chainMax += chainArt.GetComponent<ArtefactManager>().GetLevel();
                    collision.gameObject.GetComponent<EnemyInfo>().SetChainHit(1, chainMax);
                }
            }


            collision.gameObject.GetComponent<EnemyInfo>().Damage(damageDelta, crit, Color.white);
            Destroy(this.gameObject);
        }
        
        if(collision.gameObject.CompareTag("barrier"))
        {
            Destroy(this.gameObject);
        }
    }
}
