using System.Collections;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    enum CropStatus
    {
        Dirt,
        Seeded,
        Growing,
        Harvestable,
    };

    CropStatus currentStatus;
    SpriteRenderer tileSprite;

    [SerializeField]
    Sprite dirtSprite;

    [SerializeField]
    Sprite seededSprite;

    [SerializeField]
    Sprite growingSprite;

    [SerializeField]
    Sprite harvestableSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileSprite = gameObject.GetComponent<SpriteRenderer>();
        currentStatus = CropStatus.Dirt; // first status of crop
        ChangeCropVisual();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        // check states
        // based on states call the right method
        // then update the status.
        if (currentStatus == CropStatus.Dirt)
        {
            Plant();
        }
        else if (currentStatus == CropStatus.Harvestable)
        {
            Harvest();
        }
        else
        {
            Debug.Log("You cant interact");
        }
    }

    private void Plant()
    {
        // plant the seed
        currentStatus = CropStatus.Seeded;
        ChangeCropVisual();
        StartCoroutine(GrowCrop());
    }

    private void Harvest()
    {
        currentStatus = CropStatus.Dirt;
        ChangeCropVisual();
    }

    IEnumerator GrowCrop()
    {
        yield return new WaitForSeconds(5.0f);
        currentStatus = CropStatus.Growing;
        ChangeCropVisual();
        yield return new WaitForSeconds(5.0f);
        currentStatus = CropStatus.Harvestable;
        ChangeCropVisual();
    }

    private void ChangeCropVisual()
    {
        switch (currentStatus)
        {
            case CropStatus.Dirt:
                tileSprite.sprite = dirtSprite;
                break;
            case CropStatus.Seeded:
                tileSprite.sprite = seededSprite;
                break;
            case CropStatus.Growing:
                tileSprite.sprite = growingSprite;
                break;
            case CropStatus.Harvestable:
                tileSprite.sprite = harvestableSprite;
                break;
        }
    }
}
