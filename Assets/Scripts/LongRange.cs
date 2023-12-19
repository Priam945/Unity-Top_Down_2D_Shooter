using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LongRange : MonoBehaviour
{
    private bool isInLongRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsInLongRange() => isInLongRange;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isInLongRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isInLongRange = false;
        }
    }
}
