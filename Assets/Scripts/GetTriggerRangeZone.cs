using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerRangeZone : MonoBehaviour
{
    public bool isInRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Je suis rentré dans toi");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    public bool IsInRange() => isInRange;

}
