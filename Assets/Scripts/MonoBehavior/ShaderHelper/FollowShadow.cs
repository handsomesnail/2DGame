using UnityEngine;


public class FollowShadow : MonoBehaviour
{
    public SpriteRenderer upSpriteRenderer;
    public SpriteRenderer downSpriteRenderer;
    private SpriteRenderer spriteRenderer;

    private bool isUp;

    void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    void Start()
    {
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOutline();
    }

    void UpdateOutline()
    {
        upSpriteRenderer.sprite = downSpriteRenderer.sprite = spriteRenderer.sprite;
        upSpriteRenderer.flipX = downSpriteRenderer.flipX = spriteRenderer.flipX;
    }

    public void DisableAllShadow()
    {
        upSpriteRenderer.enabled = downSpriteRenderer.enabled = false;
    }
    
    public void OnChangeGravity()
    {
        isUp = !isUp;
       
    }

    public void ShowShadow()
    {
        if (isUp)
        {
            upSpriteRenderer.enabled = false;
            downSpriteRenderer.enabled = true;
        }
        else
        {
            upSpriteRenderer.enabled = true;
            downSpriteRenderer.enabled = false;
        }
    }
}