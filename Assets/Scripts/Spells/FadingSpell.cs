using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class FadingSpell : Spell
    {
        [SerializeField] private float scaleStep = 0.1f;
        [SerializeField] private float fadingSpeed = 60f;
        [SerializeField] private Transform[] fadingParts = null;

        private void Start()
        {
            scaleStep = scale.x / 10;
        }

        protected override void DestroySpell()
        {
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            while (scale.x > 0f && scale.y > 0f && scale.z > 0f)
            {
                for (int i = 0; i < fadingParts.Length; i++)
                {
                    scale.x -= scaleStep * Time.deltaTime * fadingSpeed;
                    scale.y -= scaleStep * Time.deltaTime * fadingSpeed;
                    scale.z -= scaleStep * Time.deltaTime * fadingSpeed;
                    fadingParts[i].localScale = scale;
                }
                yield return new WaitForSeconds(0.1f);
            }

            Destroy(gameObject);
        }
    }
}