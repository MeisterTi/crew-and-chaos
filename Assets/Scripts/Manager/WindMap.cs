using UnityEngine;

public class WindMap : Singleton<WindMap>
{
    public float noiseScale = 0.01f;
    public float windSpeed = 10f;
    public float rotationSpeed = 0f;
    public float timeSpeed = 0.1f;
    public float maxTimeRotation = 45f;
    public float speedTimeSpeed = 0.1f;
    public float speedAmplitude = 2f;

    protected override void Awake()
    {
        base.Awake();
    }

    public Vector3 GetWindAt(Vector3 position)
    {
        float x = position.x * noiseScale;
        float z = position.z * noiseScale;

        // Base angle from spatial noise
        float baseAngle = Mathf.PerlinNoise(x, z) * 360f;

        // Time varying rotation offset
        float timeOffset = Mathf.PerlinNoise(x + Time.time * timeSpeed, z + Time.time * timeSpeed) * maxTimeRotation;

        float angle = baseAngle + timeOffset;

        // Optional speed variation (gusts)
        float speedVariation = Mathf.PerlinNoise(x + Time.time * speedTimeSpeed, z + Time.time * speedTimeSpeed) * speedAmplitude;
        float currentWindSpeed = windSpeed + speedVariation;

        Vector3 windDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        return windDir.normalized * currentWindSpeed;
    }
}