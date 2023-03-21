using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField]
    private Transform tip;

    [SerializeField]
    private int penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPosition, _lastTouchedPosition;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchedRotation;

    void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        _tipHeight = tip.localScale.y;
    }

    void Update()
    {
        SelectColor();
        Draw();
    }

    private void SelectColor()
    {
        if (Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Color Picker"))
            {
                var tex = _touch.transform.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;

                Vector2 pos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(pos.x * tex.width - penSize/2);
                var y = (int)(pos.y * tex.height - penSize/2);

                _renderer.material.color = tex.GetPixel(x, y);
                _colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
            }

        }

    }

    private void Draw()
    {
        if (Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPosition = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPosition.x * _whiteboard.textureSize.x - penSize/2);
                var y = (int)(_touchPosition.y * _whiteboard.textureSize.y - penSize/2);

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, penSize, penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.08f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchedPosition.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchedPosition.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, _colors);
                    }

                    transform.rotation = _lastTouchedRotation;

                    _whiteboard.texture.Apply();
                }

                _lastTouchedPosition = new Vector2(x, y);
                _lastTouchedRotation = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
