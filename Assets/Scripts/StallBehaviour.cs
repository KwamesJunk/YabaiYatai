using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallBehaviour : MonoBehaviour
{
    [SerializeField] Vendor vendor;
    bool broken = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K)) {
            if (!broken) {
                broken = true;
                BreakApart(transform);
                Debug.Log("Break apart!");
                Destroy(gameObject, 3);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc) {
            if (pc.attacking)
            {
                GetComponent<BoxCollider>().enabled = false;
                transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                BreakApart(transform);
                Destroy(gameObject, 3);

                if (vendor)
                {
                    vendor.IncurWrath();
                }
            }
        }
    }

    void BreakApart(Transform node)
    {
        Rigidbody rb = node.GetComponent<Rigidbody>();
        if (rb) {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0, Random.Range(150, 400), 0));
            rb.AddTorque(new Vector3(Random.Range(-30,30), Random.Range(-30, 30), Random.Range(-30, 30)));
        }

        for (int i = 0; i < node.childCount; i++) {
            Transform childNode = node.GetChild(i);
            BreakApart(childNode);
        }
            
    }
}
