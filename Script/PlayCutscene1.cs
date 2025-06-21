using UnityEngine;
using UnityEngine.Video;

public class PlayCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}
