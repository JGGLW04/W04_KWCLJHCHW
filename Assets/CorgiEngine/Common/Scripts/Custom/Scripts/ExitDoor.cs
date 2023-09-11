using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] Sprite LightSprite;
    [SerializeField] Sprite DefaultSprite;
    List<SpriteRenderer> _lights;
    int _count;

    private void Start()
    {
        _count = 0;
        _lights = this.gameObject.GetComponentsInChildren<SpriteRenderer>().Where(sprite => sprite.name.StartsWith("Light", System.StringComparison.OrdinalIgnoreCase)).ToList();
    }
    //List<Image> selectedImage = PlayerItemUI.GetComponentsInChildren<Image>()
    //        .Where(image => image.name.EndsWith("Background", System.StringComparison.OrdinalIgnoreCase)).ToList();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;
            SetLights();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;
            SetLights();
        }
    }

    void SetLights()
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            if (i < _count / 2)
            {
                if (LightSprite != null)
                {
                    _lights[i].sprite = LightSprite;
                }
                else
                {
                    _lights[i].color = Color.red;
                }

            }
            else
            {
                if (LightSprite != null)
                {
                    _lights[i].sprite = DefaultSprite;
                }
                else
                {
                    _lights[i].color = Color.white;
                }
            }
        }
    }
}
