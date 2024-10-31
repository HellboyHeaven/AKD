using System;
using UnityEngine;

public class TransportableItem : Item
{
    [SerializeField] private string subPrompt = "Transport";

    public override string SubPrompt => subPrompt;


    public override void PickUp(IInteractor interactor)
    {
        if (interactor.InHandItem == this) return;
        gameObject.layer =  LayerMask.NameToLayer("Default");
        Rigidbody.isKinematic = true;
        transform.SetParent(interactor.InHand, true);
    }

    public override void Drop(IInteractor interactor)
    {
        gameObject.layer =  LayerMask.NameToLayer("Interaction");
        Rigidbody.isKinematic = false;
        transform.SetParent( transform.root, true);
    }
}