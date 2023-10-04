using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject mode_selector;
    public Button mode;
    private bool vis_change_mode = false;
    private bool vis_change_options = false;
    public Button mode_easy;
    public Button mode_mid;
    public Button mode_hell;
    public TextMeshProUGUI mode_perks;

    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;

    private bool can_play = false;

    public GameObject options;

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

        cloud1.gameObject.transform.position = new Vector3(cloud1.transform.position.x + 0.07f, cloud1.transform.position.y, 0);
        cloud2.gameObject.transform.position = new Vector3(cloud2.transform.position.x + 0.1f, cloud2.transform.position.y, 0);
        cloud3.gameObject.transform.position = new Vector3(cloud3.transform.position.x + 0.05f, cloud3.transform.position.y, 0);

        if (cloud1.transform.localPosition.x >= 1300f) cloud1.gameObject.transform.localPosition = new Vector3(-800f, cloud1.transform.position.y, 0);
        if (cloud2.transform.localPosition.x >= 1300f) cloud2.gameObject.transform.localPosition = new Vector3(-800f, cloud2.transform.position.y, 0);
        if (cloud3.transform.localPosition.x >= 1300f) cloud3.gameObject.transform.localPosition = new Vector3(-800f, cloud3.transform.position.y, 0);
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
