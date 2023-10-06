using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion
{
    float slowMotionTimeScale;
    float originalTimeScale;

    public void ToggleSlowMotion(bool isSlowMotion, float slowMotionTimeScale1, float originalTimeScale1)
    {
        slowMotionTimeScale = slowMotionTimeScale1;
        originalTimeScale = 1;

        if (isSlowMotion)
        {
            // Enable slow motion
            Time.timeScale = slowMotionTimeScale1;
            Time.fixedDeltaTime = slowMotionTimeScale1 * 0.02f; // Adjust fixed delta time for physics
        }
        else
        {
            // Disable slow motion
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = originalTimeScale * 0.02f; // Reset fixed delta time
        }
    }

    IEnumerator stop(float resetTime)
    {
        yield return new WaitForSecondsRealtime(resetTime);
        StopSlowMo();
    }

    void StopSlowMo()
    {
        ToggleSlowMotion(false, slowMotionTimeScale, originalTimeScale);
    }
}
