using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBeamSprite : MonoBehaviour
{
    private Animator animator;
    private StaffBlastBehavior parent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        parent = this.transform.parent.gameObject.GetComponent<StaffBlastBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation()
    {
        animator.SetTrigger("Die");
    }

    void TriggerDeath()
    {
        parent.DestroyAttack();
    }
}
