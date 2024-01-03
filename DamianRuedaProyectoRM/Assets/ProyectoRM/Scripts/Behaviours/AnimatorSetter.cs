using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Animator))]
public class AnimatorSetter : MonoBehaviour
{
    Animator _animator;
    public string field = "";

    void Start(){
        _animator = this.GetComponent<Animator>();
    }

    public void SetState(bool v){
        if(_animator == null)   _animator = this.GetComponent<Animator>();
        if(_animator != null)   _animator.SetBool(field, v);
    }

}
