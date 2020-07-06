using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration ,float magnituted)
    {
        Vector3 originalCamPots = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnituted;
            float y = Random.Range(-1, 1f) * magnituted;

            transform.localPosition = new Vector3(x, y, originalCamPots.z);

            elapsed += Time.deltaTime;

            yield return null;

        }
        transform.localPosition = originalCamPots;
    }
}
