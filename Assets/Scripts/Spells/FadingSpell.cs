using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class FadingSpell : Spell
    {
        [SerializeField] protected float scaleStep = 0.1f;
        [SerializeField] protected float fadingSpeed = 60f;

        private void Start()
        {
            scaleStep = scale.x / 10;
        }

        protected override void Destroy()
        {
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            while (scale.x > 0f && scale.y > 0f && scale.z > 0f)
            {
                scale.x -= scaleStep * Time.deltaTime * fadingSpeed;
                scale.y -= scaleStep * Time.deltaTime * fadingSpeed;
                scale.z -= scaleStep * Time.deltaTime * fadingSpeed;
                _transform.localScale = scale;
                yield return new WaitForSeconds(0.1f);
            }

            Destroy(gameObject);
        }
    }
}