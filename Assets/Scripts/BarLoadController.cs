using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLoadController : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            return;
        }

        animator.Play("bar_load");
    }

    public void OnAnimationEnd()
    {
        if (videoController.lastPlayed == "VideoPrincipalCorrida")
        {
            videoController.PlayVideoPrincipalIlha();
        }
        else if (videoController.lastPlayed == "VideoPrincipalIlha")
        {
            videoController.PlayVideoPrincipalCorrida();
        }
        gameObject.SetActive(false);
    }
}
