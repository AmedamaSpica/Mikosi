using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikosiPeopleAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;  // �A�j���[�^�[�R���|�[�l���g�擾�p
    [SerializeField] private MikosiAnomationController mikosianimationController;
    // Start is called before the first frame update
    void Start()
    {
        // �A�j���[�^�[�R���|�[�l���g�擾
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (this.transform.position.y > -0.35 && !animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", true);
            if (mikosianimationController != null)
                mikosianimationController.Jump();
        }
            
        else if (this.transform.position.y <= -0.35 && animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", false);
            if (mikosianimationController != null)
                mikosianimationController.JumpDown();
        }
          


    }
}
