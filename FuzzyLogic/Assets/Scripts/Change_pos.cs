using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_pos : MonoBehaviour
{
    [SerializeField]
    GameObject box_obj;
    [SerializeField]
    private List<GameObject> obstacle_array;
    [SerializeField]
    GameObject GoalObject;

    //Need to find a way to change positions of all the objects once the box has got close enough to the goal
    // The obstacles should change positions
    // The goal should change position based randomly from the centre
    // The player should reset to the centre - if its between a certain area or if it stops for a while - DONE

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(box_obj.transform.position, GoalObject.transform.position) < 7.25f)
        {
            box_obj.transform.position = new Vector3(0f, 0.6f, 0.32f);
            Rigidbody rigidbody_;
            rigidbody_ = box_obj.GetComponent<Rigidbody>();
            rigidbody_.velocity = new Vector3(0f, 0f, 0f);
            rigidbody_.angularVelocity = new Vector3(0f, 0f, 0f);
            box_obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
}
