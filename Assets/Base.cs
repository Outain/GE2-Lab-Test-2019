using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;
    public GameObject[] bases;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        }
        bases =  GameObject.FindGameObjectsWithTag("base");
        //this code will check for all other bases, add them to an array, then checks against the current gameObject and removes it so only enemies remain
        
        for (int i = 0; i < bases.Length; i++)
        {
            if (this.gameObject == bases[i])
            {
                ArrayUtility.Remove(ref bases, bases[i]); 
            } 
        }

        //StartCoroutine(CheckForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        tiberium += Time.deltaTime;
        text.text = "" + Mathf.RoundToInt(tiberium);
        
        if (tiberium >= 10)
        {
            SpawnShip();
            tiberium -= 10;
        }
    }

    void SpawnShip()
    {
        GameObject fighter = Instantiate(fighterPrefab, transform.position,Quaternion.identity);
        Material baseMaterial = GetComponent<Renderer>().material;
        Material fighterMaterial = fighter.GetComponent<Renderer>().material;
        Renderer []capsuleRenderer = fighter.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < capsuleRenderer.Length; i++)
        {
            capsuleRenderer[i].material.color = baseMaterial.color;
        }

        fighter.GetComponent<Arrive>().enemyBases = bases; //this populates an array of enemy base targets in the arrive function
        fighter.GetComponent<Arrive>().initialBase = this.gameObject; //this is for returning to base to refuel
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            tiberium -= 0.5f;
            Destroy(other.gameObject);
        }
    }

//
//    IEnumerator CheckForSpawn()
//    {
//        while (true)
//        {
//            if (tiberium >= 10)
//            {
//                Instantiate(fighterPrefab, transform.position,Quaternion.identity);
//                tiberium -= 10;
//                yield return new WaitForSeconds(0.1f);
//            }
//        }
//    }
}
