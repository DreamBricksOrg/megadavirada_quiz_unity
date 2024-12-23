using System.Collections;
using UnityEngine;

public class WorkaroundVideo : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    public float workaroundTimer = 130;

    private void Start()
    {
        StartCoroutine(OnAnimationFailed());
    }

    private IEnumerator OnAnimationFailed()
    {
        while (true)
        {
            yield return new WaitForSeconds(workaroundTimer);

            if (videoController.lastPlayed == "VideoPrincipalCorrida")
            {
                videoController.OnZerinhosCarraoChoose();
            }
            else if (videoController.lastPlayed == "VideoPrincipalIlha")
            {
                videoController.OnCurtirIlhaSuaChoose();
            }

            Debug.Log("Run WorkAround");
        }
    }
}
