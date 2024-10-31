 using System;
 using IPD;
 using UnityEngine;

 [RequireComponent(typeof(Animator))]
 public class Door : MonoBehaviour, IInteractable
 {
     [SerializeField] private string openPrompt;
     [SerializeField] private string closePrompt;
     [SerializeField] private string name;
     [SerializeField] private DisplaySettingsModel promptSettings;

     private bool _isOpen;
     private Animator _animator;
     
     public string Prompt => $"{(_isOpen ? closePrompt : openPrompt)} {name}";
     public Outline Outline => null;
     public DisplaySettingsModel PromptSettings => promptSettings;

     private void Start()
     {
         _animator = GetComponent<Animator>();
     }

     public void Interact(IInteractor interactor)
     {
         if (_isOpen)
         {
             _animator.ResetTrigger("Open");
             _animator.SetTrigger("Close");
             
         }
         else
         {
             _animator.ResetTrigger("Close");
             _animator.SetTrigger("Open");
             
         } 
         _isOpen = !_isOpen;
     }

     
 }