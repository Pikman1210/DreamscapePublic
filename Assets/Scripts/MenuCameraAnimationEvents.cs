using UnityEngine;

public class MenuCameraAnimationEvents : MonoBehaviour
{
    public void AnimationEnd()
    {
        // Fade to black

        GameManager.Instance.LoadScene(2); // Loads the level selector scene
    }
}
