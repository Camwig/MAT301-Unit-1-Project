using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    //Variables to keep track of the current time and the time we initial start at
    private float currentTime = 0;
    private float startingTime = 60;

    //Field for the time text object
    [SerializeField]
    Text TimetextElement;

    //Field for the time slider text object
    [SerializeField]
    TMP_Text TimeSliderText;

    //Menu game object
    [SerializeField]
    GameObject Menu;

    MenuScript menu_script;

    private void Start()
    {
        ResetTimer();
        //Retrieves the correct instance of the menu script
        menu_script = Menu.GetComponent<MenuScript>();
    }

    private void Update()
    {
        //Decrease the time as the application is played
        //(Countdown)
        currentTime -= 1 * Time.deltaTime;

        //Output the given values to the text elements
        TimetextElement.text = "Time : " + currentTime.ToString();
        TimeSliderText.text = "Set Timer : " + startingTime.ToString();

        //If the current time gets lower or equal to zero
        //The application will reset to the start menu
        if(currentTime <= 0)
        {
            TimetextElement.text = "Time : 0";
            menu_script.ToMenu();
        }
    }

    //Set the starting time value
    public void SetTimer(float new_time)
    {
        startingTime = new_time;
    }

    //Sets the current time to be that of the starting time
    public void ResetTimer()
    {
        currentTime = startingTime;
    }

    //Returns the current time
    public float GiveCurrentTime()
    {
        return currentTime;
    }
}
