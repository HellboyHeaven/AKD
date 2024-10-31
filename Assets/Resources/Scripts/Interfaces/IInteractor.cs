using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
   public Transform InHand { get; }
   public Item InHandItem { get; }
   public bool IsHandEmpty { get; }
}
