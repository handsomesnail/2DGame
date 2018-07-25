using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer followSpriteRenderer;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        spriteRenderer.sprite = followSpriteRenderer.sprite;
        spriteRenderer.flipX = followSpriteRenderer.flipX;
        spriteRenderer.flipY = followSpriteRenderer.flipY == true?false:true;
    }
}