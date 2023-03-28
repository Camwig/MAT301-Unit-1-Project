using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuzzyBox : MonoBehaviour
{
	//Engine that determines the distance/direction to the goal object
	IFuzzyEngine engineX;
	//Variables of the direction and the distance
	LinguisticVariable distance_X;
	LinguisticVariable direction_X;

	//Engine to avoid the obstacle in the x-axis
	IFuzzyEngine avoidEngineX;
	//Variables of the distance
	LinguisticVariable Avoidance_distance_X;

	//Engine that determines the distance/direction to the goal object
	IFuzzyEngine engineZ;
	//Variables of the direction and the distance
	LinguisticVariable distance_Z;
	LinguisticVariable direction_Z;

	//Engine to avoid the obstacle in the x-axis
	IFuzzyEngine avoidEngineZ;
	//Variables of the distance
	LinguisticVariable Avoidance_distance_Z;

	//Goal object
	public GameObject Goal;
	//Variable that holds the x and z positions of the goal object
	private float Goal_x;
	private float Goal_z;

	//Holds all the obstacles for the fuzzy box to avoid
	[SerializeField]
	private List<GameObject> obstacle_array;

	//Text to output the speed value
	[SerializeField]
	TMP_Text ForceSpeedText;

	//X and Z position variable
	private float Obstacle_X;
	private float Obstacle_Z;

	//Resultant value for both the X and Z axis which provides the forec to be applied to the box object to move towards the goal
	double resultX;
	double resultZ;

	//Resultant value for both the X and Z axis which provides the forec to be applied to the box object to move away from the obstacle
	double avoid_result_X;
	double avoid_result_Z;

	//Values that hold the range of the fuzzy logic an extend to from the objects position
	private float diffrence_x;
	private float diffrence_z;

	private float avoidance_x;
	private float avoidance_z;

	//Multiplier to help control the rate at which force is applied to the box object
	double speed_ = 0.65;

	//Totals both results to determine how much force to apply to the box in the corresponding axis
	double complete_resultX;
	double complete_resultZ;

	//Initial start function to setup the Rules so the Fixed update function will not run with a null set of rules
	void Start()
	{
		Setup_Fuzzy_Rules(0);
	}

	//Updates the slider text to update the speed
    void Update()
    {
		ForceSpeedText.text = "Speed of box agent : " + speed_.ToString();
    }

	//Fixed update that is used to update physics of the box object
    void FixedUpdate()
	{
		resultX = 0.0;
		resultZ = 0.0;

		avoid_result_X = 0.0;
		avoid_result_Z = 0.0;

		//Defuzzify the engines to get a representable value
		resultX = engineX.Defuzzify(new { distanceX = (double)this.transform.position.x });
		resultZ = engineZ.Defuzzify(new { distanceZ = (double)this.transform.position.z });

		//TrapezoidCoGDefuzzification;
		//CoGDefuzzification;
		//MoMDefuzzification;

		avoid_result_X = avoidEngineX.Defuzzify(new { Avoidance_distanceX = (double)this.transform.position.x });
		avoid_result_Z = avoidEngineZ.Defuzzify(new { Avoidance_distanceZ = (double)this.transform.position.z });

		//Combine the results
		complete_resultX = resultX + avoid_result_X;
		complete_resultZ = resultZ + avoid_result_Z;

		//Apply the complete result in force multiplied by the speed value to the box object
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.AddForce(new Vector3((float)(complete_resultX * speed_), 0f, (float)(complete_resultZ * speed_)));
    }

	public void Setup_Fuzzy_Rules(int pos_in_arr)
    {
		//Sets the valuses for the following variables
		Goal_x = Goal.transform.position.x;
		Goal_z = Goal.transform.position.z;
		diffrence_x = 200;
		diffrence_z = 200;
		avoidance_x = 6.25f;
		avoidance_z = 6.25f;
		Obstacle_X = obstacle_array[pos_in_arr].transform.position.x;
		Obstacle_Z = obstacle_array[pos_in_arr].transform.position.z;

		// Here we need to setup the Fuzzy Inference System
		//Sets up the linguistic variable
		distance_X = new LinguisticVariable("distanceX");
        //Sets up the shapes for the fuzzy logic graph



        //AddTrapezoid
        var right_X = distance_X.MembershipFunctions.AddTrapezoid("right_X", Goal_x - diffrence_x, Goal_x - diffrence_x, Goal_x - 10, Goal_x - 1);
        var none_X = distance_X.MembershipFunctions.AddTrapezoid("none_X", Goal_x - 10, Goal_x - 0.5, Goal_x + 0.5, Goal_x + 10);
        var left_X = distance_X.MembershipFunctions.AddTrapezoid("left_X", Goal_x + 1, Goal_x + 10, Goal_x + diffrence_x, Goal_x + diffrence_x);

        //AddTriangle
        //var right_X = distance_X.MembershipFunctions.AddTriangle("right_X", Goal_x - diffrence_x, Goal_x - diffrence_x, Goal_x - 1);
        //var none_X = distance_X.MembershipFunctions.AddTriangle("none_X", Goal_x - 10, Goal_x, Goal_x + 10);
        //var left_X = distance_X.MembershipFunctions.AddTriangle("left_X", Goal_x + 1, Goal_x + diffrence_x, Goal_x + diffrence_x);

        //AddRectangle
        //var right_X = distance_X.MembershipFunctions.AddRectangle("right_X", Goal_x - diffrence_x, Goal_x - 1);
        //var none_X = distance_X.MembershipFunctions.AddRectangle("none_X", Goal_x - 10, Goal_x + 10);
        //var left_X = distance_X.MembershipFunctions.AddRectangle("left_X", Goal_x + 1, Goal_x + diffrence_x);

        direction_X = new LinguisticVariable("directionX");

        //AddTrapezoid
        var right_direction_X = direction_X.MembershipFunctions.AddTrapezoid("right_direction_X", Goal_x + -diffrence_x, Goal_x + -diffrence_x, Goal_x + -10, Goal_x + -1);
        var none_direction_X = direction_X.MembershipFunctions.AddTrapezoid("none_direction_X", Goal_x + -10, Goal_x + -0.5, Goal_x + 0.5, Goal_x + 10);
        var left_direction_X = direction_X.MembershipFunctions.AddTrapezoid("left_direction_X", Goal_x + 1, Goal_x + 10, Goal_x + diffrence_x, Goal_x + diffrence_x);

        //AddTriangle
        //var right_direction_X = direction_X.MembershipFunctions.AddTriangle("right_direction_X", Goal_x - diffrence_x, Goal_x - diffrence_x, Goal_x - 1);
        //var none_direction_X = direction_X.MembershipFunctions.AddTriangle("none_direction_X", Goal_x - 10, Goal_x, Goal_x + 10);
        //var left_direction_X = direction_X.MembershipFunctions.AddTriangle("left_direction_X", Goal_x + 1, Goal_x + diffrence_x, Goal_x + diffrence_x);

        //AddRectangle
        //var right_direction_X = direction_X.MembershipFunctions.AddRectangle("right_direction_X", Goal_x - diffrence_x, Goal_x - 1);
        //var none_direction_X = direction_X.MembershipFunctions.AddRectangle("none_direction_X", Goal_x - 10, Goal_x + 10);
        //var left_direction_X = direction_X.MembershipFunctions.AddRectangle("left_direction_X", Goal_x + 1, Goal_x + diffrence_x);

        //Initialise the given engine
        engineX = new FuzzyEngineFactory().Default();

        //Sets up the variable rules for the engine
        //Will determine the state of one linguistic variable based off the state of another
        //So since distance represnts how far from the object the box is
        //So if the box sits to the right of the goal it should move in the left direction
        var rule1_X = Rule.If(distance_X.Is(right_X)).Then(direction_X.Is(left_direction_X));
		var rule2_X = Rule.If(distance_X.Is(left_X)).Then(direction_X.Is(right_direction_X));
		var rule3_X = Rule.If(distance_X.Is(none_X)).Then(direction_X.Is(none_direction_X));

		//Add the rules to the engine
		engineX.Rules.Add(rule1_X, rule2_X, rule3_X);

		//Repeats the same process for diffrent engines
		Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");


        //AddTrapezoid
        var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X + -avoidance_x, Obstacle_X + -avoidance_x, Obstacle_X + -1.25, Obstacle_X + -0.25);
        var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X + 0.25, Obstacle_X + 1.25, Obstacle_X + avoidance_x, Obstacle_X + avoidance_x);

        //AddTriangle
        //var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTriangle("right_avoidance_distanceX", Obstacle_X + -avoidance_x, Obstacle_X + -avoidance_x, Obstacle_X + -1);
        //var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTriangle("left_avoidance_distanceX", Obstacle_X + 0.25, Obstacle_X + avoidance_x, Obstacle_X + avoidance_x);

        //AddRectangle
        //var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddRectangle("right_avoidance_distanceX", Obstacle_X + -avoidance_x, Obstacle_X + -1);
        //var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddRectangle("left_avoidance_distanceX", Obstacle_X + 0.25, Obstacle_X + avoidance_x);

        avoidEngineX = new FuzzyEngineFactory().Default();

		//For the obstacle we want to do the opposite of the goal
		//as in we want to move away from it 
		//so if the box is to the right of the obstacle keep moving to the right
		var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
		var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));

		avoidEngineX.Rules.Add(rule4_X, rule5_X);

		distance_Z = new LinguisticVariable("distanceZ");

        //AddTrapezoid
        var right_Z = distance_Z.MembershipFunctions.AddTrapezoid("right_Y", Goal_z - diffrence_z, Goal_z - diffrence_z, Goal_z - 10, Goal_z - 1);
        var none_Z = distance_Z.MembershipFunctions.AddTrapezoid("none_Y", Goal_z - 10, Goal_z - 0.5, Goal_z + 0.5, Goal_z + 10);
        var left_Z = distance_Z.MembershipFunctions.AddTrapezoid("left_Y", Goal_z + 1, Goal_z + 10, Goal_z + diffrence_z, Goal_z + diffrence_z);

        //AddTrangle
        //var right_Z = distance_Z.MembershipFunctions.AddTriangle("right_Y", Goal_z - diffrence_z, Goal_z - diffrence_z, Goal_z - 1);
        //var none_Z = distance_Z.MembershipFunctions.AddTriangle("none_Y", Goal_z - 10, Goal_z, Goal_z + 10);
        //var left_Z = distance_Z.MembershipFunctions.AddTriangle("left_Y", Goal_z + 1, Goal_z + diffrence_z, Goal_z + diffrence_z);

        //AddRectangle
        //var right_Z = distance_Z.MembershipFunctions.AddRectangle("right_Y", Goal_z - diffrence_z, Goal_z - 1);
        //var none_Z = distance_Z.MembershipFunctions.AddRectangle("none_Y", Goal_z - 10, Goal_z + 10);
        //var left_Z = distance_Z.MembershipFunctions.AddRectangle("left_Y", Goal_z + 1, Goal_z + diffrence_z);

        direction_Z = new LinguisticVariable("directionZ");

        //AddTrapezoid
        var right_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("right_direction_Y", Goal_z + -diffrence_z, Goal_z + -diffrence_z, Goal_z + -10, Goal_z + -1);
        var none_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("none_direction_Y", Goal_z + -10, Goal_z + -0.5, Goal_z + 0.5, Goal_z + 10);
        var left_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("left_direction_Y", Goal_z + 1, Goal_z + 10, Goal_z + diffrence_z, Goal_z + diffrence_z);

        //AddTriangle
        //var right_direction_Z = direction_Z.MembershipFunctions.AddTriangle("right_direction_Y", Goal_z - diffrence_z, Goal_z - diffrence_z, Goal_z - 1);
        //var none_direction_Z = direction_Z.MembershipFunctions.AddTriangle("none_direction_Y", Goal_z - 10, Goal_z, Goal_z + 10);
        //var left_direction_Z = direction_Z.MembershipFunctions.AddTriangle("left_direction_Y", Goal_z + 1, Goal_z + diffrence_z, Goal_z + diffrence_z);

        //AddRectangle
        //var right_direction_Z = direction_Z.MembershipFunctions.AddRectangle("right_direction_Y", Goal_z - diffrence_z, Goal_z - 1);
        //var none_direction_Z = direction_Z.MembershipFunctions.AddRectangle("none_direction_Y", Goal_z - 10, Goal_z + 10);
        //var left_direction_Z = direction_Z.MembershipFunctions.AddRectangle("left_direction_Y", Goal_z + 1, Goal_z + diffrence_z);

        engineZ = new FuzzyEngineFactory().Default();

		var rule1_Z = Rule.If(distance_Z.Is(right_Z)).Then(direction_Z.Is(left_direction_Z));
		var rule2_Z = Rule.If(distance_Z.Is(left_Z)).Then(direction_Z.Is(right_direction_Z));
		var rule3_Z = Rule.If(distance_Z.Is(none_Z)).Then(direction_Z.Is(none_direction_Z));

		engineZ.Rules.Add(rule1_Z, rule2_Z, rule3_Z);

		Avoidance_distance_Z = new LinguisticVariable("Avoidance_distanceZ");

        //AddTrapezoid
        var right_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("right_avoidance_distanceZ", Obstacle_Z + -avoidance_z, Obstacle_Z + -avoidance_z, Obstacle_Z + -1.25, Obstacle_Z + -0.25);
        var left_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("left_avoidance_distanceZ", Obstacle_Z + 0.25, Obstacle_Z + 1.25, Obstacle_Z + avoidance_z, Obstacle_Z + avoidance_z);

        //AddTriangle
        //var right_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTriangle("right_avoidance_distanceZ", Obstacle_Z + -avoidance_z, Obstacle_Z + -avoidance_z, Obstacle_Z + -1);
        //var left_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTriangle("left_avoidance_distanceZ", Obstacle_Z + 0.25, Obstacle_Z + avoidance_z, Obstacle_Z + avoidance_z);

        //AddRectangle
        //var right_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddRectangle("right_avoidance_distanceZ", Obstacle_Z + -avoidance_z, Obstacle_Z + -1);
        //var left_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddRectangle("left_avoidance_distanceZ", Obstacle_Z + 0.25, Obstacle_Z + avoidance_z);

        avoidEngineZ = new FuzzyEngineFactory().Default();

		var rule4_Z = Rule.If(Avoidance_distance_Z.Is(right_avoidance_distance_z)).Then(direction_Z.Is(right_direction_Z));
		var rule5_Z = Rule.If(Avoidance_distance_Z.Is(left_avoidance_distance_z)).Then(direction_Z.Is(left_direction_Z));

		avoidEngineZ.Rules.Add(rule4_Z, rule5_Z);
	}

	//Sets the new speed value based off the input from the slider
	public void NewSpeed(float new_speed)
    {
		speed_ = new_speed;
    }

}
