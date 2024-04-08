using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsFusion : MonoBehaviour
{
    public int gemsIndex;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gems"))
        {
            GemsFusion collideGems = collision.gameObject.GetComponent<GemsFusion>();

            if (collideGems.gemsIndex == gemsIndex)
            {
                if (!gameObject.activeSelf || !collision.gameObject.activeSelf)
                {
                    return;
                }
                print("same gems");
                collision.gameObject.SetActive(false);
                Destroy((collision.gameObject));
                GameObject nextGems = Instantiate(GameManager.instance.AllGems[gemsIndex + 1]);
                nextGems.transform.position = transform.position;
                
                gameObject.SetActive(false);
                Destroy(gameObject);

            }
        }
    }
}