using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0;
    private float startingTime = 60;

    [SerializeField]
    Text textElement;

    [SerializeField]
    TMP_Text TimeSliderText;

    [SerializeField]
    GameObject Menu;

    MenuScript menu_script;

    private void Start()
    {
        //currentTime = startingTime;
        //fuzzy_box = box_obj.GetComponent<FuzzyBox>();
        ResetTimer();
        menu_script = Menu.GetComponent<MenuScript>();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        textElement.text = "Time : " + currentTime.ToString();
        TimeSliderText.text = "Set Timer : " + startingTime.ToString();

        if(currentTime <= 0)
        {
            textElement.text = "Time : 0";
            menu_script.ToMenu();
            //Time.timeScale = 0.0f;
            //For back on its set back to one.
        }
        //print(currentTime);
    }


    public void SetTimer(float new_time)
    {
        startingTime = new_time;
    }

    public void ResetTimer()
    {
        currentTime = startingTime;
    }

    public float GiveCurrentTime()
    {
        return currentTime;
    }
}
