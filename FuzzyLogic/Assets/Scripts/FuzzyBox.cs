using FLS;
using FLS.Rules;
using UnityEngine;

public class FuzzyBox : MonoBehaviour
{

	bool selected = false;

	//Need to fold the multiple engines into one

	IFuzzyEngine engineX;
	LinguisticVariable distance_X;
	LinguisticVariable direction_X;

	IFuzzyEngine engineZ;
	LinguisticVariable distance_Z;
	LinguisticVariable direction_Z;

	public GameObject Centre;
	private float Centre_x;
	private float Centre_z;

	void Start()
	{
		Centre_x = Centre.transform.position.x;

		// Here we need to setup the Fuzzy Inference System
		distance_X = new LinguisticVariable("distanceX");
		var right_X = distance_X.MembershipFunctions.AddTrapezoid("right_X", Centre_x - 75, Centre_x - 75, Centre_x - 5, Centre_x - 1);
		var none_X = distance_X.MembershipFunctions.AddTrapezoid("none_X", Centre_x - 1, Centre_x - 0.5, Centre_x + 0.5, Centre_x + 1);
		var left_X = distance_X.MembershipFunctions.AddTrapezoid("left_X", Centre_x + 1, Centre_x + 5, Centre_x + 75, Centre_x + 75);

		direction_X = new LinguisticVariable("directionX");
		var right_direction_X = direction_X.MembershipFunctions.AddTrapezoid("right_direction_X", Centre_x + -75, Centre_x + -75, Centre_x + -5, Centre_x + -1);
		var none_direction_X = direction_X.MembershipFunctions.AddTrapezoid("none_direction_X", Centre_x + -1, Centre_x + -0.5, Centre_x + 0.5, Centre_x + 1);
		var left_direction_X = direction_X.MembershipFunctions.AddTrapezoid("left_direction_X", Centre_x + 1, Centre_x + 5, Centre_x + 75, Centre_x + 75);

		engineX = new FuzzyEngineFactory().Default();

		var rule1_X = Rule.If(distance_X.Is(right_X)).Then(direction_X.Is(left_direction_X));
		var rule2_X = Rule.If(distance_X.Is(left_X)).Then(direction_X.Is(right_direction_X));
		var rule3_X = Rule.If(distance_X.Is(none_X)).Then(direction_X.Is(none_direction_X));

		engineX.Rules.Add(rule1_X, rule2_X, rule3_X);

		Centre_z = Centre.transform.position.z;

		// Here we need to setup the Fuzzy Inference System
		distance_Z = new LinguisticVariable("distanceZ");
		var right_Z = distance_Z.MembershipFunctions.AddTrapezoid("right_Y", Centre_z - 50, Centre_z - 50, Centre_z - 5, Centre_z - 1);
		var none_Z = distance_Z.MembershipFunctions.AddTrapezoid("none_Y", Centre_z - 5, Centre_z - 0.5, Centre_z + 0.5, Centre_z + 5);
		var left_Z = distance_Z.MembershipFunctions.AddTrapezoid("left_Y", Centre_z + 1, Centre_z + 5, Centre_z + 50, Centre_z + 50);

		direction_Z = new LinguisticVariable("directionZ");
		var right_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("right_direction_Y", Centre_z + -50, Centre_z + -50, Centre_z + -5, Centre_z + -1);
		var none_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("none_direction_Y", Centre_z + -5, Centre_z + -0.5, Centre_z + 0.5, Centre_z + 5);
		var left_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("left_direction_Y", Centre_z + 1, Centre_z + 5, Centre_z + 50, Centre_z + 50);

		engineZ = new FuzzyEngineFactory().Default();

		var rule1_Z = Rule.If(distance_Z.Is(right_Z)).Then(direction_Z.Is(left_direction_Z));
		var rule2_Z = Rule.If(distance_Z.Is(left_Z)).Then(direction_Z.Is(right_direction_Z));
		var rule3_Z = Rule.If(distance_Z.Is(none_Z)).Then(direction_Z.Is(none_direction_Z));

		engineZ.Rules.Add(rule1_Z, rule2_Z, rule3_Z);
	}

	void FixedUpdate()
	{
        //if(!selected && this.transform.position.y < 0.6f)
        //{
        // Convert position of box to value between 0 and 100
        double resultX = 0.0;
        double resultZ = 0.0;

        resultX = engineX.Defuzzify(new { distanceX = (double)this.transform.position.x });

        resultZ = engineZ.Defuzzify(new { distanceZ = (double)this.transform.position.z });

        //Need to defuziffy the if valriable for the rules

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(new Vector3((float)(resultX * 0.8), 0f, (float)(resultZ * 0.8)));
        //}
    }

    // Update is called once per frame
    void Update()
	{
        if (Input.GetMouseButtonDown(0))
        {
            var hit = new RaycastHit();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "FuzzyBox") Debug.Log("You have clicked the FuzzyBox");
                selected = true;
            }
        }

        if (Input.GetMouseButton(0) && selected)
        {
            float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            transform.position = new Vector3(curPosition.x, Mathf.Max(0.5f, curPosition.y), transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }
    }


}
