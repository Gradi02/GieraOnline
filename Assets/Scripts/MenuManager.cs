using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Modes")]
    public GameObject mode_selector;
    public Button mode;
    private bool vis_change_mode = false;
    private bool vis_change_options = false;
    public Button mode_easy;
    public Button mode_mid;
    public Button mode_hell;
    public TextMeshProUGUI mode_perks;

    [Header("Clouds")]
    public GameObject[] clouds;
    public float[] cloud_speed;

    [Header("Options")] 
    public GameObject options;
    private bool can_play = false;

    public RawImage trans;
    private bool transition = false;
    private float duration;

    private void Start()
    {
        duration = 0;
        Button btn_easy = mode_easy.GetComponent<Button>();
        btn_easy.onClick.AddListener(Easytext);
        Button btn_mid = mode_mid.GetComponent<Button>();
        btn_mid.onClick.AddListener(Midtext);
        Button btn_hell = mode_hell.GetComponent<Button>();
        btn_hell.onClick.AddListener(Helltext);

        mode_selector.SetActive(false);
        options.SetActive(false);


        mode_perks.text = "";

        trans.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (can_play)
        {
            SceneManager.LoadScene("map");
        }


        if (transition)
        {
                    duration += + Time.deltaTime;
                    RawImage rawImage = trans.GetComponent<RawImage>();
                    Color currentColor = rawImage.color;
                    float alphaValue = Mathf.Lerp(0, 1, 0.1f * duration * 5);
                    currentColor.a = alphaValue;
                    rawImage.color = currentColor;
            if(alphaValue==1) can_play = true;
        }

            //dzia³aj¹ce chmury ez
          /* foreach(GameObject c in clouds)
            {
                c.transform.localPosition += transform.right * Time.deltaTime * cloudSpeed;

                if (c.transform.localPosition.x >= 1300)
                    c.transform.localPosition -= new Vector3(2300, 0, 0);
            } */

                for(int i = 0; i<4; i++)
                {
                    clouds[i].transform.localPosition += transform.right * Time.deltaTime * cloud_speed[i];
                }
        }

    public void Easytext() {
        mode_perks.text = "hp: +5\r\ntime: -5";       
    }
    public void Midtext() {
        mode_perks.text = "hp: +0\r\ntime: -0";       
    }
    public void Helltext() {
        mode_perks.text = "hp: -10\r\ntime: +10";       
    }
    public void Menu_Play()
    {
        trans.gameObject.SetActive(true);
        transition = true;
    }

    public void Menu_Mode()
    {
        vis_change_options = false;
        options.SetActive(false);
        if (vis_change_mode == true)
        {
            mode_selector.SetActive(false);
            vis_change_mode = false;
        }
        else if (vis_change_mode == false)
        {
            mode_selector.SetActive(true);
            vis_change_mode = true;
        }
    }

    public void Menu_options()
    {
        vis_change_mode = false;
        mode_selector.SetActive(false);
        if (vis_change_options == true)
        {
            options.SetActive(false);
            vis_change_options = false;
        }
        else if (vis_change_options == false)
        {
            options.SetActive(true);
            vis_change_options = true;
        }
    }

    public void Menu_Quit()
    {
        Application.Quit();
    }
}
