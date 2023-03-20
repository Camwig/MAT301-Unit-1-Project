using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    [SerializeField]
    GameObject timer;
    CountdownTimer this_timer;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);


        this_timer = timer.GetComponent<CountdownTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void StartApp()
    {
        //Set all the appropriate things and then run
        //Timer - Done
        //Objects
        //Points

        //-----Timer set----
        this_timer.SetTimer(10.0f);
        this_timer.ResetTimer();
        //-----------------

        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
