using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public GameObject bullet;
    public float tiberium = 7;
    public Arrive aScript;

    //public GameObject currentTarget;
    // Start is called before the first frame update
    void Awake()
    {
       // bullet.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

    }

    private void Start()
    {
        aScript = GetComponent<Arrive>();
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        if (tiberium <= 0)
        {
            GetComponent<Seek>().enabled = true;
        }
        Vector3 targetDir = aScript.targetGameObject.transform.position - transform.position;
        Vector3.RotateTowards(transform.forward,targetDir,Time.deltaTime,0);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            tiberium--;
            yield return new WaitForSeconds(1f);
        }
    }
 }
