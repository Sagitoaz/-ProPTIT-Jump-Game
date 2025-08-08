using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPaddle : PaddleController
{
    protected override void CalculateMovement()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
        if (transform.position.x <= _screenLeft)
        {
            _direction = Vector2.right;
            StartDamageRoutine();
        }
        else if (transform.position.x >= _screenRight)
        {
            _direction = Vector2.left;
            StartDamageRoutine();
        }
    }
    private void StartDamageRoutine()
    {
        if (_canDamage)
        {
            Damage(1);
        }
    }
    protected override void Damage(int dmg)
    {
        _health -= dmg;
        if (_health > 0)
        {
            StartCoroutine(DamageCoolDown(0.5f));
        }
        if (_health == 1)
        {
            _spriteRenderer.sprite = _spriteImgs[1];
        }
        else if (_health <= 0)
        {
            StopAllCoroutines();
            DestroyPaddle();
        }
    }
}
