using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Clouds")]
    public GameObject[] clouds;
    public float[] cloud_speed;

    [Header("Options")] 
    private bool vis_change_options = false;
    public GameObject options;
    private bool can_play = false;

    public Slider music;
    public Slider sounds;

    public RawImage trans;
    private bool transition = false;
    private float duration;



    private void Start()
    {
        duration = 0;

        

        //mode_selector.SetActive(false);
        options.SetActive(false);

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

        if (vis_change_options)
        {
            PlayerPrefs.SetFloat("volume_music", music.value / 100);
            PlayerPrefs.SetFloat("volume_sounds", sounds.value / 100);
        }
    }
    public void Menu_Play()
    {
        FindObjectOfType<AudioManager>().Play("click");
        trans.gameObject.SetActive(true);
        transition = true;

        options.SetActive(false);
        vis_change_options = false;
    }

    public void Menu_options()
    {
        FindObjectOfType<AudioManager>().Play("click");
        //mode_selector.SetActive(false);
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

        music.value = PlayerPrefs.GetFloat("volume_music") * 100;
        sounds.value = PlayerPrefs.GetFloat("volume_sounds") * 100;
    }

    public void Menu_Quit()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Application.Quit();
    }
}
