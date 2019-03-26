
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using Random = System.Random;


public class Seek : SteeringBehaviour
{
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    public GameObject[] enemyBases;
    public Fighter fScript;

    private void Start()
    {
       
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);    
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }

       
       
    }

   
}