using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public enum VendorState
    {
        IDLE,
        ANGRY,
        DAMAGED,
        DEAD
    };

    [SerializeField] protected Animator simpleHumanAnimator;
    [SerializeField] float speed = 5.0f;
    int animIndex = 0;
    int[] animList = { 9, 10, 11, 12, 16, 7, 8 };
    public VendorState state = VendorState.IDLE;
    PlayerController player;
    CharacterController characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayNextIdleAnimation();
        player = GameObject.FindFirstObjectByType<PlayerController>();
        characterController = GetComponent<CharacterController>();
    }

    float tk = 0;
    Vector3 target;
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case VendorState.IDLE:
                if (Time.time > tk)
                {
                    tk = Time.time + Random.Range(2.0f, 5.0f);
                    PlayNextIdleAnimation();
                }
                break;
            case VendorState.ANGRY:
                if (Time.time > tk)
                {
                    tk = Time.time + Random.Range(1.0f, 3.0f);
                    target = player.transform.position;
                    transform.LookAt(player.transform, Vector3.up);
                }
                characterController.Move(Vector3.Normalize(target - transform.position) * (speed * Time.deltaTime));
                
                break;
            case VendorState.DAMAGED:
                if (Time.time > tk) {
                    transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                    state = VendorState.ANGRY;
                }
                break;
            case VendorState.DEAD:
                break;
        }
        
    }

    void PlayNextIdleAnimation()
    {
        ++animIndex;
        if (animIndex >= 7)
        {
            animIndex = 0;
        }

        simpleHumanAnimator.SetInteger("arms", animList[animIndex]);
        simpleHumanAnimator.SetInteger("legs", animList[animIndex]);
    }

    public void IncurWrath()
    {
        if (state == VendorState.IDLE)
        {
            state = VendorState.ANGRY;
            tk = 0;
            GetComponent<CharacterController>().enabled = true;
            simpleHumanAnimator.SetInteger("arms", 2);
            simpleHumanAnimator.SetInteger("legs", 2);
        }
    }

    public void TakeDamage()
    {
        if (state == VendorState.ANGRY) {
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
            state = VendorState.DAMAGED;
            tk = Time.time + 2.0f;
        }
    }
}
