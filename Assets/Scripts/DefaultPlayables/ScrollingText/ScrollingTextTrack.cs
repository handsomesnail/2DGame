using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;


    [TrackColor(0.7794118f, 0.4002983f, 0.1547362f)]
    [TrackClipType(typeof(ScrollingTextClip))]
    [TrackBindingType(typeof(Text))]
    public class ScrollingTextTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<ScrollingTextMixerBehaviour>.Create (graph, inputCount);
        }
    }