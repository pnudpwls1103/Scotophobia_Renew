using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    float direction;
    float speed;
    [SerializeField]
    float bulbOnSpeed;
    [SerializeField]
    float bulbOffSpeed;
    static bool bulb = false;
    public static event System.Action OnBulbOn = null;
    public static event System.Action OnBulbOff = null;

    private const float SCAN_DISTANCE = 2f;
    public const float maxDistance = 9f;
    public GameObject scanObject;
    public GameObject scanClickObject;
    private Collider2D scanCollider;
    private IInteraction scanInteraction;

    Rigidbody2D rigid;

    void Start()
    {
        speed = bulb ? bulbOnSpeed : bulbOffSpeed;
        rigid = GetComponent<Rigidbody2D>();
        OnBulbOn += SpeedUp;
        OnBulbOff += SpeedDown;
    }

    void Update()
    {
        //transform.Find("Inventory").gameObject.activeSelf
        Move();

        GetScanObjectMouse();

        if (scanClickObject)
            Interact();
        if (Input.GetKeyDown(KeyCode.I))
            UI_Root.TogglePopup(typeof(Define.UI_Popup), (int)Define.UI_Popup.Inventory);
        if (Input.GetKeyDown(KeyCode.O))
            ToggleBulb();

    }

    private void ScanObject()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 2);
        Vector2 rayDirection = new Vector2(direction, 0);

        Debug.DrawRay(rayOrigin, rayDirection * SCAN_DISTANCE, Color.red);

        scanObject = null;
        scanInteraction = null;
        scanCollider = null;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, SCAN_DISTANCE, LayerMask.GetMask("Object"));

        if (hit.collider != null)
        {
            scanObject = hit.transform.gameObject;
            Debug.Log(scanObject.name);

            scanCollider = hit.collider;
            scanInteraction = hit.collider.GetComponent<IInteraction>();
        }
    }

    private void GetScanObjectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Object"));

            if (hit.collider != null)
            {
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                if (distance <= maxDistance)
                    scanClickObject = hit.collider.gameObject;

                return;
            }
        }

        scanClickObject = null;
    }

    void Interact()
    {
        if (scanCollider != null && scanInteraction != null)
            scanInteraction.Interact(gameObject);
    }
    public void ToggleBulb()
    {
        if (bulb)
        {
            OnBulbOff();
        }

        else
        {
            OnBulbOn();
        }

        bulb = !bulb;
    }
    void GetItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1000f);
        foreach (Collider2D col in colliders)
            if (col.GetComponent<Item>() != null)
            {
                Debug.Log($"Item {col.name} Get!");
                Inventory.Instance.Insert(col.name);
                return;
            }
    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");

        rigid.position += Vector2.right * hAxis * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
            Die();
    }

    public void Die()
    {

    }
    void SpeedUp()
    {
        speed = bulbOnSpeed;
    }
    void SpeedDown()
    {
        speed = bulbOffSpeed;
    }

}