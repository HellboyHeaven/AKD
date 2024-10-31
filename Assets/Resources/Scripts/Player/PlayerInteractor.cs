using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform inHand;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float hitRange;
    [SerializeField] private InputActionReference interactInput;
    [SerializeField] private Camera camera;
    private int _numFound;
    private bool _isHandEmpty;
    private IInteractable _interactable;


    private UIManager _uiManager;


    public Transform InHand => inHand;
    public Item InHandItem => _item;
    public bool IsHandEmpty => _isHandEmpty;
    private Item _item;

    [Inject]
    private void Construct(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    private void OnDrawGizmos()
    {
        var pos = camera.transform.position;
        var dir = camera.transform.forward;
        Gizmos.DrawLine(pos, pos + dir * hitRange);
    }

    private void Start()
    {
        interactInput.action.performed += Interact;
    }

    private void Update()
    {
        InteractRayCast();

        if (_interactable != null)
            _uiManager.ShowPrompt(_interactable);
        else
            _uiManager.HidePrompt();
    }


    private void InteractRayCast()
    {
        var cameraTransfrom = camera.transform;

        RaycastHit hit;
        if (Physics.Raycast(cameraTransfrom.position, cameraTransfrom.forward, out hit, hitRange, layerMask))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (_interactable != interactable && _interactable != null)
            {
                OutlineOff(_interactable);
            }

            if (interactable != null)
            {
                OutlineOn(interactable);
                _interactable = interactable;
            }
          
            return;
        }
       
        if (_interactable != null)
        {
            OutlineOff(_interactable);
        }

        _interactable = null;


    }

    private void OutlineOn(IInteractable interactable)
    {
        var outline = interactable.Outline;
        if (outline != null) outline.enabled = true;
    }
    private void OutlineOff(IInteractable interactable)
    {
        var outline = interactable.Outline;
        if (outline != null) outline.enabled = false;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (_interactable == null || _interactable is Item)
        {
            Drop(_item);
            
        }

        if (_interactable == null) return;
        
        _interactable.Interact(this);

        if (_interactable is Item)
        {
            _item = _interactable as Item;
        }
    }

    private void Drop(Item item)
    {
        if (item == null) return;
        item.Drop(this);
        _item = null;
    }

    void OnDestroy()
    {
        interactInput.action.performed -= Interact;
    }
}