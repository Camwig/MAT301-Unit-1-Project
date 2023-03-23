using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //Menu Object
    public GameObject menu;

    //Timer object and timer script
    [SerializeField]
    GameObject timer;
    CountdownTimer this_timer;

    //Position object and position script
    [SerializeField]
    GameObject PositionObj;
    Change_pos pos_script;

    //Game manager object and point system script
    [SerializeField]
    GameObject GameManager;
    PointSystem points_;

    //Text element
    [SerializeField]
    Text textElement;

    void Start()
    {
        //Sets up the scripts to be the appropriate instance of each script 
        //from the appropriate object
        this_timer = timer.GetComponent<CountdownTimer>();
        pos_script = PositionObj.GetComponent<Change_pos>();
        points_ = GameManager.GetComponent<PointSystem>();
        ToMenu();

    }

    public void ToMenu()
    {
        float newAverage = 0.0f;

        //Calculates the mean of all the timing values collected throughout runtime
        //Aslong as the timings list has more than zero values
        if(pos_script.Timings.Count !=0)
        {
            //Get the mean
            for (int j = 0; j < pos_script.Timings.Count; j++)
            {
                newAverage += pos_script.Timings[j];
            }

            newAverage = newAverage / pos_script.Timings.Count;
        }

        //Gives the avarege time it took to reach the goal to the text output
        textElement.text = "Average Time to reach goal : " +  newAverage.ToString();

        //Sets the menu panel to active
        menu.SetActive(true);
        //Pauses the application
        Time.timeScale = 0.0f;
    }

    public void StartApp()
    {
        ResetVariables();
        //Unpauses the application
        //and removes the menu oanel from the screen
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResetVariables()
    {
        //Resets the variables for the next instance of the application
        this_timer.ResetTimer();

        pos_script.ResetBox();
        pos_script.ResetOtherObjects();
        pos_script.ResetCollisions();

        float newAverage = 0;

        textElement.text = "Avergae time to reach goal : " + newAverage.ToString();

        points_.ResetValues();
    }
}
