using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator simpleHumanAnimator;
    [SerializeField] GameObject stallPrefab;
    [SerializeField] float speed = 10.0f;
    float currentSpeed = 10.0f;
    public bool attacking = false;
    int numCollisions = 0;
    public bool receiveInput = false;

    // Start is called before the first frame update
    void Start()
    {
        simpleHumanAnimator.SetInteger("arms", 33);
        simpleHumanAnimator.SetInteger("legs", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!receiveInput) return;

        GetComponent<CharacterController>().Move(new Vector3(0, -5.0f, 0)); // Gravity
        if (numCollisions <= 0) {
            currentSpeed = speed;
        }
        else {
            currentSpeed = speed / 2.0f;
        }

        float horizInput = Input.GetAxis("Horizontal");
        if (horizInput!=0.0f)
            Debug.Log(horizInput);


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //transform.position = transform.position + (speed * Time.deltaTime * transform.forward);
            GetComponent<CharacterController>().Move(currentSpeed * Time.deltaTime * transform.forward);
            simpleHumanAnimator.SetInteger("legs", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //transform.position = transform.position - (speed * Time.deltaTime * transform.forward);
            GetComponent<CharacterController>().Move(-currentSpeed * Time.deltaTime * transform.forward);
            simpleHumanAnimator.SetInteger("legs", 1);
        }
        else
        {
            //simpleHumanAnimator.SetInteger("legs", 33);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 180.0f * Time.deltaTime, 0));
            simpleHumanAnimator.SetInteger("legs", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -180.0f * Time.deltaTime, 0));
            simpleHumanAnimator.SetInteger("legs", 1);
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            simpleHumanAnimator.SetInteger("arms", 34);
            attacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            simpleHumanAnimator.SetInteger("arms", 33);
            attacking = false;
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (!GameObject.FindObjectOfType<StallBehaviour>())
        //    {


        //        Instantiate(stallPrefab, transform.position + new Vector3(5, .5f, 5), Quaternion.identity);
        //    }
        //}
        //if (Input.GetKey(KeyCode.M))
        //{
        //    GetComponent<CharacterController>().Move(speed * Time.deltaTime * transform.forward);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Vendor vendor = other.gameObject.GetComponent<Vendor>();
        if (vendor) {
            if (vendor.state == Vendor.VendorState.ANGRY || vendor.state == Vendor.VendorState.DAMAGED) {
                ++numCollisions;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Vendor vendor = other.gameObject.GetComponent<Vendor>();

        if (vendor) {
            if (vendor.state == Vendor.VendorState.ANGRY || vendor.state == Vendor.VendorState.DAMAGED) {
                --numCollisions;
            }
        }
    }
}
