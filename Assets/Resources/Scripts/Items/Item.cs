using IPD;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Outline))]
public abstract class Item : MonoBehaviour, IInteractable
{
    [SerializeField] protected string prompt;
    [SerializeField] private string name;
    [SerializeField] private DisplaySettingsModel promptSettings;
    [SerializeField] protected float DropForce;
    
    protected Rigidbody Rigidbody;
    private Outline _outline;
    public virtual string SubPrompt => prompt;
    public DisplaySettingsModel PromptSettings => promptSettings;
    public Outline Outline => _outline;
    public string Prompt => $"{SubPrompt} {name}";

    public void Interact(IInteractor interactor) {
        gameObject.layer =  LayerMask.NameToLayer("Default");
        PickUp(interactor);
    }

    private void Start()
    {
        _outline = GetComponent<Outline>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public abstract void PickUp(IInteractor interactor);
    
    public abstract void Drop(IInteractor interactor);

}