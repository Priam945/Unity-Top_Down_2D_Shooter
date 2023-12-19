using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRange : MonoBehaviour
{
    private bool isInShortRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsInShortRange() => isInShortRange;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isInShortRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isInShortRange = false;
        }
    }
}
