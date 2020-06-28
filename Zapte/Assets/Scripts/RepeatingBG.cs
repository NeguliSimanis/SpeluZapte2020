using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    [SerializeField]
    GameObject copy;
    [SerializeField]
    bool shouldRespawnInsteadOfTeleport;
    bool destroying = false;
   	public float speed;
	public float endX;
	public float startX;

    [SerializeField]
    bool isTree;
    [SerializeField]
    Sprite[] treeOptions;
    SpriteRenderer objectSprite;

    private void Start()
    {
        objectSprite = gameObject.GetComponent<SpriteRenderer>();
        InitializeObject(false);
    }

    private void Update(){
		transform.Translate(Vector2.left * speed * Time.deltaTime);
		
		if(transform.position.x <= endX){
            InitializeObject(true);
		}
	}

    private void InitializeObject(bool setPos)
    {
        if (setPos && !shouldRespawnInsteadOfTeleport && !destroying)
        {
            Vector2 pos = new Vector2(startX, transform.position.y);
            transform.position = pos;
        }
        if (isTree)
        {
            objectSprite.sprite = treeOptions[Random.Range(0, treeOptions.Length)];
        }
        if (shouldRespawnInsteadOfTeleport && !destroying && setPos)
        {
            Vector2 pos = new Vector2(startX, transform.position.y);
            Instantiate(copy, pos, Quaternion.identity, null);
            destroying = true;
            StartCoroutine(DestroyAfterSeconds());
        }
    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(130f);
        Destroy(gameObject);
    }
}
