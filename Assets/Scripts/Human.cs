using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnHit()
    {
        Debug.Log("Human is Hit!");
        GameManager.Instance.GameOver();
    }

    private void HandleDeadEffects()
    {
        animator.SetTrigger("Dead");
    }
}
