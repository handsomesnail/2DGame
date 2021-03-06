using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


    public class ScrollingTextMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Text trackBinding = playerData as Text;

            if (!trackBinding)
                return;

            int inputCount = playable.GetInputCount ();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<ScrollingTextBehaviour> inputPlayable = (ScriptPlayable<ScrollingTextBehaviour>)playable.GetInput(i);
                ScrollingTextBehaviour input = inputPlayable.GetBehaviour ();

                if (Mathf.Approximately (inputWeight, 1f))
                {
                    string message = input.GetMessage ((float)inputPlayable.GetTime ());
                    trackBinding.text = message;
                }
            }
        }
    }
