
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Arrive: SteeringBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float slowingDistance = 15.0f;

    public GameObject targetGameObject = null;
    public GameObject initialBase;
    
    public GameObject[] enemyBases;
    public Fighter fScript;
    public bool refueling;
        
    public override Vector3 Calculate()
    {
        return boid.ArriveForce(targetPosition, slowingDistance);
    }

    private void Start()
    {
        fScript = GetComponent<Fighter>();
        ChooseTarget();
    }

    public void Update()
    {
        if (refueling && initialBase !=null)
        {
            targetPosition = initialBase.transform.position;
        }
        else if (targetGameObject != null)
        {
            targetPosition = targetGameObject.transform.position;
        }

        if (!refueling)
        {
            if (Vector3.Distance(targetGameObject.transform.position, transform.position) <= 10)
            {
                fScript.enabled = true;

                boid.velocity = new Vector3(0, 0, 0);
                //GetComponent<Arrive>().enabled = false;
            }
        }

        if (refueling)
        {
            fScript.enabled = false;
            if (Vector3.Distance(initialBase.transform.position, transform.position) <= 2)
            {
                if (initialBase.GetComponent<Base>().tiberium >= 7)
                {
                    fScript.tiberium = 7;
                    initialBase.GetComponent<Base>().tiberium -= 7;
                    refueling = false;
                }
            }
        }
    }
    
    public void ChooseTarget()
    {
        int i = Random.Range(0, enemyBases.Length);
        targetGameObject = enemyBases[i];
    }
}