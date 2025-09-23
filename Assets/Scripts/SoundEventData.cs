public class SoundEventData
{
    public SoundType Type;
    public float Volume;
    public bool Loop;
    public bool DuckMusic;
    public float DuckVolume;
    public float DuckDuration;
    public float StartTime;

    public SoundEventData(
        SoundType type,
        float volume = 1f,
        bool loop = false,
        bool duckMusic = false,
        float duckVolume = 0.3f,
        float duckDuration = 0.5f,
        float startTime = 0f)
    {
        Type = type;
        Volume = volume;
        Loop = loop;
        DuckMusic = duckMusic;
        DuckVolume = duckVolume;
        DuckDuration = duckDuration;
        StartTime = startTime;
    }
}