using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    [System.Serializable]
    public class AudioCrossfader
    {
        public List<CrossfadeSection> clips = new List<CrossfadeSection>();
        [Range(0, 1)] public float intensity;
        public AnimationCurve crossfadeCurve;
        public AudioMngr.Type type;
        public (CrossfadeSection, CrossfadeSection) OldSection = (null, null);

        public void Setup()
        {
            clips.ForEach(section =>
            {
                section.source = AudioMngr.Player(type).Play(section.clip);
                section.source.source.volume = 0f;
            });
        }

        public void FreeSources()
        {
            clips.ForEach(clip =>
            {
                if (clip.source != null)
                {
                    AudioMngr.Player(type).FreeSource(clip.source, clip.source.playId);
                }
            });
        }

        public void Update()
        {
            if (clips.Count <= 1) return;//Se necesitan al menos dos clips para hacer crossfade
            var current = GetSectionClips();

            if (OldSection.Item1 != null && OldSection.Item1 != current.Item1 && OldSection.Item1 != current.Item2)
                OldSection.Item1.source.source.volume = 0f;

            if (OldSection.Item2 != null && OldSection.Item2 != current.Item1 && OldSection.Item2 != current.Item2)
                OldSection.Item2.source.source.volume = 0f;

            float clip1Volume = current.Item1.clip.volume * (1 - crossfadeCurve.Evaluate(SectionIntensity));
            float clip2Colume = current.Item2.clip.volume * crossfadeCurve.Evaluate(SectionIntensity);
            current.Item1.source.source.volume = clip1Volume;
            current.Item2.source.source.volume = clip2Colume;
            OldSection = current;
        }

        public float SectionIntensity
        {
            get
            {
                return (intensity - CurrentSection * SectionMaxIntensity) / SectionMaxIntensity;
            }
        }

        public float SectionMaxIntensity => 1f / SectionsCount;

        public int SectionsCount => clips.Count - 1;

        public int CurrentSection
        {
            get
            {
                int currentSection = (int)(SectionsCount * intensity);
                if (currentSection >= SectionsCount) currentSection = SectionsCount - 1;
                return currentSection;
            }
        }

        public (CrossfadeSection, CrossfadeSection) GetSectionClips()
        {
            return (clips[CurrentSection], clips[CurrentSection + 1]);
        }
    }

    [System.Serializable]
    public class CrossfadeSection
    {
        public AudioSourceInfo source;
        public ClipInfo clip;
    }
}