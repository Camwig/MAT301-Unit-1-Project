using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0;
    private float startingTime = 100;

    [SerializeField]
    Text textElement;

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
        textElement.text = currentTime.ToString();

        if(currentTime <= 0)
        {
            textElement.text = "0";
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
}