using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    private float Overall_points;
    private float Positive_points;
    private float Negative_points;

    [SerializeField]
    Text Overall_point_text;
    [SerializeField]
    Text Positive_point_text;
    [SerializeField]
    Text Negative_point_text;

    public void Start()
    {
        ResetValues();
    }

    public void Update()
    {
        Positive_point_text.text = Positive_points.ToString();
        Negative_point_text.text = Negative_points.ToString();
        Overall_point_text.text = Overall_points.ToString();
    }

    public void AddPoints()
    {
        Positive_points += 2.5f;
    }

    public void MinusPoints()
    {
        Negative_points += 1f;
    }

    public void CollectPoints()
    {
        Overall_points += Positive_points;
        Overall_points -= Negative_points;
        Positive_points = 0.0f;
        Negative_points = 0.0f;
    }

    public void ResetValues()
    {
        Overall_points = 0.0f;
        Positive_points = 0.0f;
        Negative_points = 0.0f;
    }

    public void ResetPoints()
    {
        Positive_points = 0.0f;
        Negative_points = 0.0f;
    }
}
