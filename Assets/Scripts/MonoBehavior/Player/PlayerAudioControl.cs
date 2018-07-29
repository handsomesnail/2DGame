using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
    public RandomAudioClipPlayer footStepAudio;
    public RandomAudioClipPlayer jumpInToHoleAudio;
    public RandomAudioClipPlayer jumpOnFloorStepAudio;
    public RandomAudioClipPlayer deadAudio;
    
    public void PlayFootStepAudio()
    {
        footStepAudio.PlayRandomSound();
    }

    public void PlayJumpOnFloorAudio()
    {
        jumpOnFloorStepAudio.PlayRandomSound();
    }

    public void PlayJumpInToHoleAudio()
    {
        jumpInToHoleAudio.PlayRandomSound();
    }

    public void PlayDeadAudio()
    {
        deadAudio.PlayRandomSound();
    }

}
