using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    //Variable values to keep track of the points
    private float Overall_points;
    private float Positive_points;
    private float Negative_points;

    //Text object
    [SerializeField]
    Text Overall_point_text;
    [SerializeField]
    Text Negative_point_text;

    public void Start()
    {
        //Sets the initial values of the points
        ResetValues();
    }

    public void Update()
    {
        //Outputs the point values to the text element
        Negative_point_text.text = "Negative Obstacle Modifier : " + Negative_points.ToString();
        Overall_point_text.text = "Overall Score : " + Overall_points.ToString();
    }

    public void AddPoints()
    {
        //Incriments points
        Positive_points += 2.5f;
    }

    public void MinusPoints()
    {
        //Incriments points
        Negative_points += 1f;
    }

    public void CollectPoints()
    {
        //Takes away the negative points and adds the positive ones
        Overall_points -= Negative_points;
        Overall_points += Positive_points;
        ResetPoints();
    }

    public void ResetValues()
    {
        //Resets the point variables
        Overall_points = 0.0f;
        Positive_points = 0.0f;
        Negative_points = 0.0f;
    }

    public void ResetPoints()
    {
        //Resets only the temporary positive and negative point variables
        Positive_points = 0.0f;
        Negative_points = 0.0f;
    }
}
