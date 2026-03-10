using UnityEngine;


[System.Serializable]
public class ParallaxLayer
{
    public float speedX = 0.5f;
    public float speedY = 0.2f;

    private Transform _transform;
    private Vector3 _targetPosition;

    private SpriteRenderer _sprite;
    private float _spriteWidth;
    private bool _infinteX;

    public ParallaxLayer(Transform t)
    {

        _transform = t;
        _targetPosition = t.position;

        _sprite = t.GetComponent<SpriteRenderer>();

        if (_sprite != null)
        {
            _spriteWidth = _sprite.bounds.size.x;
            // _infinteX = _spriteWidth > 0f; 
        }


        var settings = t.GetComponent<ParallaxLayerSetting>();
        if (settings != null)
        {
            speedX = settings.speedX;
            speedY = settings.speedY;
        }
    }

    public void Move (Vector3 delta, bool Vertical, float smoothing)
    {
        float moveX = delta.x * (1f - speedX);
        float moveY = Vertical ? delta.y * (1f - speedY) : 0f;

        _targetPosition += new Vector3(moveX, moveY, 0f);
        _transform.position = smoothing > 0f ? Vector3.Lerp(_transform.position, _targetPosition, smoothing) : _targetPosition;

        if (_infinteX)
        {
            WrapHorizontal();
        }
    }
    
    private void WrapHorizontal()
    {
        float camX = Camera.main.transform.position.x;
        float diffX = camX - _transform.position.x;

        if (Mathf.Abs(diffX) >= _spriteWidth)
        {
            float offset = diffX > 0 ? _spriteWidth : -_spriteWidth;
            _transform.position += new Vector3(offset, 0f, 0f);
        }
    }




}

 