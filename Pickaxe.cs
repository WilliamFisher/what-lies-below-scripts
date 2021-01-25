using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private Animator _animator;
    private Camera _myCam;
    [SerializeField]
    private float _damageAmount = 5;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _myCam = GetComponentInParent<Camera>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        _animator.SetBool("Attack", true);
        Ray ray = _myCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3.0f))
        {
            if(hit.transform.tag == "Enemy")
            {
                try
                {
                    CharacterBase character = gameObject.GetComponentInParent<CharacterBase>();
                    hit.transform.gameObject.GetComponent<IEnemyDamagable>().TakeDamage(_damageAmount, character.gameObject);
                } catch (Exception e)
                {
                    Debug.Log("Exception in pickaxe: " + e);
                }
                
            }
        }
    }
}
