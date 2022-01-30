using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsFadeOut : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 3f;
    [SerializeField] private float _delayBeforeFade = 5f;
    [SerializeField] private Image _image = null;
    [SerializeField] private Image _image2 = null;
    [SerializeField] private Image _image3 = null;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_delayBeforeFade);

        Color c = _image.color;
        float alpha = c.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / _fadeTime;
            c.a = alpha;
            _image.color = c;
            _image2.color = c;
            _image3.color = c;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
