using UnityEngine;

public class BGRepeater : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private BGMover bg;
    [SerializeField] private Transform bgStartPosition;
    private CustomObjectPool<BGMover> bgPool;

    private void OnEnable()
    {
        bgPool = new CustomObjectPool<BGMover>(Preload, GetAction, ReleaseAction, 3);
        bgPool.Get();
    }

    private BGMover Preload() => Instantiate(bg, canvas);
    private void GetAction(BGMover bg) 
    { 
        bg.gameObject.SetActive(true); 
        bg.gameObject.transform.position = bgStartPosition.position; 
    
    }
    private void ReleaseAction(BGMover bg) => bg.gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BGMover bg = collision.gameObject.GetComponent<BGMover>();
        if (bg == null) return;
        bgPool.Release(bg);
        bgPool.Get();
    }
}
