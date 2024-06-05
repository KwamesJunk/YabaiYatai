using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    [SerializeField]GameObject owner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Vendor vendor = other.GetComponent<Vendor>();
       
        if (vendor && owner.GetComponent<PlayerController>().attacking) {
            vendor.TakeDamage();
        }
    }
}
