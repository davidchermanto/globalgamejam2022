using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private GameObject cardFolder;
    [SerializeField] private GameObject cardActiveFolder;

    [Header("Dependencies")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerManager playerManager;

    [Header("Current Data")]
    [SerializeField] private List<CardDisplay> activeCards;

    [Header("Drag Resources")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int currentDraggedId;
    [SerializeField] private bool draggingCard;
    [SerializeField] private Vector3 startDrag;
    [SerializeField] private Vector3 endDrag;

    private Vector3 cameraOffset = new Vector3(0, 0, 10);

    [Header("Stored Data")]
    [SerializeField] private Sprite lightCard;
    [SerializeField] private Sprite darkCard;
    [SerializeField] private int currentId;

    [Header("Pre-stored positions")]
    private Vector3 cardScale = new Vector3(1.2f, 1f);
    private Vector3 cardScaleLarge = new Vector3(1.44f, 1.2f);
    private float slideUpDistance = 1.6f;
    private List<List<Vector3>> cardPos = new List<List<Vector3>>
    {
        new List<Vector3>
        {
            new Vector3(-160, 95, 12),
            new Vector3(-80, 115, 6),
            new Vector3(0, 122, 0),
            new Vector3(80, 115, -6),
            new Vector3(160, 95, -12)
        },
        new List<Vector3>
        {
            new Vector3(-128.5f, 100, 6),
            new Vector3(-44.5f, 120, 3),
            new Vector3(44.5f, 120, -3),
            new Vector3(128.5f, 100, -6),
        },
        new List<Vector3>
        {
            new Vector3(-90, 116, 10),
            new Vector3(0, 126, 0),
            new Vector3(90, 116, -10),
        },
        new List<Vector3>
        {
            new Vector3(-44.5f, 120, 3),
            new Vector3(44.5f, 120, -3),
        },
        new List<Vector3>
        {
            new Vector3(0, 123, 0)
        }
    };

    public void Setup()
    {
        activeCards = new List<CardDisplay>();
    }

    private void Update()
    {
        if (draggingCard)
        {
            if (Input.GetMouseButton(0))
            {
                endDrag = mainCamera.ScreenToWorldPoint(Input.mousePosition) + cameraOffset;
                lineRenderer.SetPosition(1, endDrag);
            }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;

                // get enemy using raycast
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        OnTryUseCard(currentDraggedId, hit.transform.GetComponent<EnemyDisplayer>());
                    }
                    else
                    {
                        ShrinkCard(currentDraggedId);
                    }
                }
                else
                {
                    ShrinkCard(currentDraggedId);
                }
            }
        }
    }

    // Arranges cards in canvas
    public void ArrangeCards()
    {
        for(int i = 0; i < activeCards.Count; i++)
        {
            activeCards[i].transform.SetParent(cardFolder.transform);
            activeCards[i].transform.localPosition = new Vector3(cardPos[5 - activeCards.Count][i].x, cardPos[5 - activeCards.Count][i].y - 50);
            
            activeCards[i].transform.rotation = Quaternion.Euler(0, 0, cardPos[5 - activeCards.Count][i].z);
            activeCards[i].transform.localScale = cardScale;
        }
    }

    public void AddCards(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            AddCard(card);
        }
    }

    public bool AddCard(Card card)
    {
        if(activeCards.Count > 5)
        {
            return false;
        }

        GameObject cardObject = Instantiate(cardPrefab);
        cardObject.transform.SetParent(cardFolder.transform);

        CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
        cardDisplay.UpdateValues(card.GetIcon(), card.GetText(), card.GetOrbValue(), 
            GetCardByColor(card.GetIsLight()), currentId, card.GetIsLight(), card.GetTitle());
        card.displayId = currentId;

        EventTrigger eventTrigger = cardObject.GetComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDown.callback.AddListener(delegate { OnStartDragCard(card.displayId); });
        eventTrigger.triggers.Add(pointerDown);

        activeCards.Add(cardDisplay);

        currentId++;

        ArrangeCards();

        return true;
    }

    public void UpdateCard(Card card)
    {
        GetCardByID(card.displayId).UpdateValues(card.GetIcon(), card.GetText(), card.GetOrbValue(),
            GetCardByColor(card.GetIsLight()), card.displayId, card.GetIsLight(), card.GetTitle());
    }

    public void DestroyAllCards()
    {
        for(int i = activeCards.Count; i > 0; i--)
        {
            DestroyCard(activeCards[0]);
        }
    }

    public void DestroyCard(int id)
    {
        CardDisplay cardDisplay = GetCardByID(id);

        if(cardDisplay != null)
        {
            activeCards.Remove(cardDisplay);

            StartCoroutine(FadeCardOut(cardDisplay));
        }
    }

    public void DestroyCard(CardDisplay cardDisplay)
    {
        if (cardDisplay != null)
        {
            activeCards.Remove(cardDisplay);

            StartCoroutine(FadeCardOut(cardDisplay));
        }
    }

    private IEnumerator FadeCardOut(CardDisplay cardDisplay)
    {
        float timer = 0;
        float duration = 0.25f;

        Destroy(cardDisplay.GetComponent<Button>());
        Destroy(cardDisplay.GetComponent<EventTrigger>());

        CanvasGroup canvasGroup = cardDisplay.GetComponent<CanvasGroup>();

        while(timer < 1)
        {
            timer += Time.deltaTime / duration;

            canvasGroup.alpha = Mathf.Lerp(1, 0, timer);

            yield return new WaitForEndOfFrame();
        }

        Destroy(cardDisplay.gameObject);
    }

    public void OnStartDragCard(int cardId)
    {
        draggingCard = true;
        currentDraggedId = cardId;

        // Enables line renderer
        startDrag = mainCamera.ScreenToWorldPoint(Input.mousePosition) + cameraOffset;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startDrag);

        // expand the card and push it up a bit
        ExpandCard(cardId);
    }

    private void ExpandCard(int cardId)
    {
        CardDisplay cardDisplay = GetCardByID(cardId);

        cardDisplay.transform.SetParent(cardActiveFolder.transform);
        cardDisplay.transform.localScale = cardScaleLarge;
        cardDisplay.transform.position = new Vector3(cardDisplay.transform.position.x, cardDisplay.transform.position.y * slideUpDistance);
    }

    private void ShrinkCard(int cardId)
    {
        CardDisplay cardDisplay = GetCardByID(cardId);

        cardDisplay.transform.SetParent(cardFolder.transform);
        cardDisplay.transform.localScale = cardScale;

        ArrangeCards();
    }

    public void OnTryUseCardUntargeted(int id)
    {
        draggingCard = false;

        if (!playerManager.GetActiveCards()[id].GetIsPassive() && !playerManager.GetActiveCards()[id].GetIsSingleTarget())
        {
            playerManager.UseCard(id);

            DestroyCard(id);
        }
        else
        {
            ShrinkCard(id);
        }

        ArrangeCards();
    }

    public void OnTryUseCard(int id, EnemyDisplayer enemyDisplayer)
    {
        draggingCard = false;

        if (!playerManager.GetActiveCardByIndex(id).GetIsPassive() && playerManager.GetActiveCardByIndex(id).GetIsSingleTarget())
        {
            playerManager.UseCardTargetted(id, enemyDisplayer.GetEnemy());

            if (playerManager.GetActiveCardByIndex(id) != null)
            {
                DestroyCard(id);
            }
        }
        else if (!playerManager.GetActiveCardByIndex(id).GetIsPassive() && !playerManager.GetActiveCardByIndex(id).GetIsSingleTarget())
        {
            playerManager.UseCard(id);

            if(playerManager.GetActiveCardByIndex(id) != null)
            {
                DestroyCard(id);
            }
        }
        else
        {
            ShrinkCard(id);
        }

        ArrangeCards();
    }

    private Sprite GetCardByColor(bool isLight)
    {
        if (isLight)
        {
            return lightCard;
        }
        else
        {
            return darkCard;
        }
    }

    private CardDisplay GetCardByID(int id)
    {
        foreach(CardDisplay cardDisplay in activeCards)
        {
            if(cardDisplay.id == id)
            {
                return cardDisplay;
            }
        }

        Debug.LogWarning("Requested card that doesnt exist: "+id);

        return null;
    }

    public int GetCardsCount()
    {
        return activeCards.Count;
    }
}
