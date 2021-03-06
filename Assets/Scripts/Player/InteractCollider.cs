﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{
    public List<Transform> possibleinteracts;
    public string[] tags;
    public string[] layers;

    void Start(){
        possibleinteracts = new List<Transform>{};
    }
    
    void Update(){
        List<int> indexes = new List<int>{};
        for (int i = 0; i < possibleinteracts.Count; i++)
        {
            if (possibleinteracts[i]){
                if (!TagMatch(possibleinteracts[i].tag)){ 
                    //indexes.Add(OnArray(possibleinteracts[i]));
                    indexes.Add(i);
                }
            }
            else{
                //possibleinteracts.RemoveAt(i);
                indexes.Add(i);
            }
        }
        foreach (int i in indexes)
        {
            possibleinteracts.RemoveAt(i);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (this.enabled){
            if ((TagMatch(other.tag) && LayerMatch(LayerMask.LayerToName(other.gameObject.layer))) && OnArray(other.transform)==-1){
                //small test (if on other playable cahracter then get only if is on the right hand)
                if (!other.GetComponentInParent<InteractControl>())
                    possibleinteracts.Add(other.transform);
                else {
                    InteractControl i = other.GetComponentInParent<InteractControl>();
                    if (i.rightGrab == other.transform || (i.leftGrab == other.transform && !i.rightGrab))
                    possibleinteracts.Add(other.transform);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled){
            if (OnArray(other.transform)!=-1){
                possibleinteracts.RemoveAt(OnArray(other.transform));
            }
        }
    }

    bool TagMatch (string s){
        foreach (string t in tags)
        {
            if (s==t) return true;
        }
        return false;
    }

    bool LayerMatch (string s){
        foreach (string l in layers)
        {
            if (s==l) return true;
        }
        return false;
    }
    

    int OnArray(Transform t){
        for (int i = 0; i < possibleinteracts.Count; i++)
        {
            if (possibleinteracts[i]==t) return i;
        }
        return -1;
    }
}
