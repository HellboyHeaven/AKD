using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class TrunkTrigger : MonoBehaviour
{
    [SerializeField] private int itemToCount;
    
    private List<Item> _items = new List<Item>();
    public UnityEvent allItemCollected;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item == null) return; 
        _items.Add(item);
        if (_items.Count >= itemToCount) allItemCollected.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if (item == null) return; 
        _items.Remove(item);
    }
}
