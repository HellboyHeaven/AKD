using System.Collections;
using System.Collections.Generic;
using IPD;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    string Prompt { get; }
    void Interact(IInteractor interactor);
    DisplaySettingsModel PromptSettings {get;}
    Outline Outline { get; }
}
