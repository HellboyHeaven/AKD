using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PickUpItem : Item
{
    [SerializeField] private string subPrompt = "Pick up";

    public override string SubPrompt => subPrompt;
    

    public override void PickUp(IInteractor interactor)
    {
        if (interactor.InHandItem == this) return;
        gameObject.layer =  LayerMask.NameToLayer("Default");
        Rigidbody.isKinematic = true;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.SetParent(interactor.InHand, false);
    }

    public override void Drop(IInteractor interactor) {
        gameObject.layer =  LayerMask.NameToLayer("Interaction");
        Rigidbody.isKinematic = false;
        transform.SetParent( transform.root, true);
        Rigidbody.AddForce(transform.forward * DropForce);
    }
}