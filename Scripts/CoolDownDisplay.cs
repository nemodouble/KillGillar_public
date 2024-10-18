using System.Collections;
using System.Collections.Generic;
using Boss.Pattern;
using UnityEngine;

public class CoolDownDisplay : MonoBehaviour
{
    private BossPattern _bossPattern;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _bossPattern = transform.parent.parent.GetComponent<BossPattern>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bossPattern.IsCoolDown())
            _spriteRenderer.color = new Color(163/255f, 221/255f, 199/255f, 220/255f);
        else
            _spriteRenderer.color = new Color(226/255f, 128/255f, 128/255f, 220/255f);
    }
}
