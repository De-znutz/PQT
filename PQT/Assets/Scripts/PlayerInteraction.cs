using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private Transform playerTransform;
    //--------------------------------------------------
    // Other Vars
    RaycastHit hitObject;
    private bool interactQueued;
    private Interactable hitObjectInteractable;
    //--------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check 'E' key
        if (Input.GetKeyDown(KeyCode.E)) {
            interactQueued = true;
        }
        
    }

    //Called once per physics frame
    void FixedUpdate() {
        //Check for Interact bool
        if (interactQueued) {
            interactQueued = false;
            //Cast a Sphere from behind the player mesh position, of radius Xf, going forward, stores a RaycastHit, goes for length Xf, and uses LayerMask
            //returns true if it hits something
            if (Physics.SphereCast(playerTransform.transform.position+playerTransform.forward*-0.8f, 0.8f, playerTransform.forward, out hitObject, 2f, interactionMask))
            {
                Debug.Log("Interacting blyat");
                //Tries to get an Interactable component from the hit Object. If there is such a component, calls the interaction script.
                hitObjectInteractable = hitObject.transform.GetComponent<Interactable>();
                if (hitObjectInteractable != null)
                {
                    hitObjectInteractable.Interact();
                }


            }
        
        }
        




    }
    //Debug to see SphereCast

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(playerTransform.transform.position, playerTransform.transform.position + playerTransform.forward * -1f);
        Gizmos.DrawWireSphere(playerTransform.transform.position + playerTransform.forward * -1f, 0.8f);
    }

}
