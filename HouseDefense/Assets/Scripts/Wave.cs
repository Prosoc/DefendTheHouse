using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{
    public List<WaveSegment> segments = new List<WaveSegment>();

    public Wave(WaveSegment segment)
    {
        AddSegment(segment);
    }

    public Wave(List<WaveSegment> segments)
    {
        AddSegments(segments);
    }

    public Wave()
    {
    }

    public void AddSegment(WaveSegment segment)
    {
        segments.Add(segment);
    }
    public void AddSegments(List<WaveSegment> segments)
    {
        segments.AddRange(segments);
    }

    public void RemoveSegment(WaveSegment segment)
    {
        segments.Remove(segment);
    }
}
