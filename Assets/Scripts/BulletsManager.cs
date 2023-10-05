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
    void Start()
    {
        Destroy(this.gameObject, 10);
        player = GameObject.FindGameObjectWithTag("Player");

        ParticleSystem ps = bulletparticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule col = bulletparticle.main;
        //col.startColor = player.GetComponent<ChangeMode>().GetColor();
        info = player.GetComponent<PlayerInfo>();
        sparky = player.transform.GetChild(1).transform.Find("Sparky").gameObject;
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

            collision.gameObject.GetComponent<EnemyInfo>().Damage(damageDelta, crit, Color.white);
            /*
            GameObject DmgPopup = Instantiate(pfDamagePopup, collision.transform.position, Quaternion.identity);

            //Zadawanie Damage
            if (collision.gameObject.GetComponent<EnemyInfo>().protection > 0)
            {
                damageDelta = 1;
                crit = false;
                DmgPopup.GetComponent<TextMeshPro>().color = Color.grey;
                collision.gameObject.GetComponent<EnemyInfo>().protection -= 1;
            }
            else 
            {
                collision.gameObject.GetComponent<EnemyInfo>().health -= damageDelta;
            }
            
            //PopUp
            DmgPopup.GetComponent<TextMeshPro>().text = damageDelta.ToString();
            DmgPopup.GetComponent<DmgPopup>().SetVelocity(bulletSpeed * Time.deltaTime * transform.right);

            if(damageDelta > (info.GetDamage() * (info.GetMultiplier() - (0.25f * info.GetMultiplier()))))
            {
                DmgPopup.GetComponent<TextMeshPro>().color = Color.yellow;
            }

            if (crit)
            {
                DmgPopup.GetComponent<TextMeshPro>().color = Color.red;
                DmgPopup.GetComponent<TextMeshPro>().fontStyle = TMPro.FontStyles.Bold;
                DmgPopup.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }
            */
            Destroy(this.gameObject);
        }

    }
}
