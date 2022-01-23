using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    IEnumerator Shake(float duration, float magnitude) {
        var origPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, origPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origPos;
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            StartCoroutine(Shake(0.3f, 0.2f));
        }
    }
}
