using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
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
        

    private bool easy;
    private bool mid;
    private bool hell;

    public TextMeshProUGUI mode_perks;




    public GameObject options;



    private void Start()
    {
        Button btn_easy = mode_easy.GetComponent<Button>();
        btn_easy.onClick.AddListener(Easytext);
        Button btn_mid = mode_mid.GetComponent<Button>();
        btn_mid.onClick.AddListener(Midtext);
        Button btn_hell = mode_hell.GetComponent<Button>();
        btn_hell.onClick.AddListener(Helltext);

        mode_selector.SetActive(false);
        options.SetActive(false);


        mode_perks.text = "";
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
        SceneManager.LoadScene("map");    
            
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
