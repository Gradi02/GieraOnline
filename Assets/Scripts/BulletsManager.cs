using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletsManager : MonoBehaviour
{
    public float bulletSpeed = 15;
    public ParticleSystem bulletparticle;
    private GameObject player;
    private EnemyInfo.types currentMode; // 1-Air > 2-Water > 3-Fire > 4-Nature
    private PlayerInfo info;

    [SerializeField] private GameObject pfDamagePopup;
    void Start()
    {
        Destroy(this.gameObject, 10);
        player = GameObject.FindGameObjectWithTag("Player");

        ParticleSystem ps = bulletparticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule col = bulletparticle.main;
        col.startColor = player.GetComponent<ChangeMode>().GetColor();
        info = player.GetComponent<PlayerInfo>();
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
        currentMode = player.GetComponent<ChangeMode>().GetMode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool crit = false;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageDelta = (int)Mathf.Round(info.GetDamage() * Random.Range(1.0f, (float)info.GetMultiplier()));
            if (Random.Range(0, 100) <= info.GetCritChance())
            {
                damageDelta *= info.GetCritMulti();
                crit = true;
            }

            if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Air)
            {
                if (currentMode == EnemyInfo.types.Air) damageDelta /= 4;
                else if (currentMode == EnemyInfo.types.Nature) damageDelta *= 2;
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Water)
            {
                if (currentMode == EnemyInfo.types.Water) damageDelta /= 4;
                else if (currentMode == EnemyInfo.types.Air) damageDelta *= 2;
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Fire)
            {
                if (currentMode == EnemyInfo.types.Fire) damageDelta /= 4;
                else if (currentMode == EnemyInfo.types.Water) damageDelta *= 2;
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Nature)
            {
                if (currentMode == EnemyInfo.types.Nature) damageDelta /= 4;
                else if (currentMode == EnemyInfo.types.Fire) damageDelta *= 2;
            }

            collision.gameObject.GetComponent<EnemyInfo>().health -= damageDelta;
            GameObject DmgPopup = Instantiate(pfDamagePopup, collision.transform.position, Quaternion.identity);
            DmgPopup.GetComponent<TextMeshPro>().text = damageDelta.ToString();
            DmgPopup.GetComponent<DmgPopup>().SetVelocity(bulletSpeed * Time.deltaTime * transform.right);

            if (crit)
            {
                DmgPopup.GetComponent<TextMeshPro>().color = Color.red;
                DmgPopup.GetComponent<TextMeshPro>().fontStyle = TMPro.FontStyles.Bold;
                DmgPopup.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }

            Destroy(this.gameObject);
        }

    }
}
