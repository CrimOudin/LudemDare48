using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public List<ThingToBuy> thingsToBuy;
    public List<Transform> selectionOptions;

    public GameObject fakeShip;
    public GameObject shopWindow;

    public Transform shipStart;
    public Transform shipEnd;

    public GameObject arrow;

    private int currentSelection = 0;
    private bool hasControl = false;
    private float timeBetweenSelections = 0f;

    private void Awake()
    {
        StartCoroutine(WorldManager.Instance.FadeScreen(true, () =>
        {
            StartCoroutine(MoveTransformToPositionAtSpeed(fakeShip.transform, shipEnd.position, 200, () => { ShowShop(); }));
        }));
    }

    private void Update()
    {
        if(hasControl)
        {
            if (timeBetweenSelections <= 0)
            {
                float vert = Input.GetAxisRaw("Vertical");
                bool moved = false;
                if (vert > 0 && currentSelection > 0)
                {
                    currentSelection--;
                    moved = true;
                    timeBetweenSelections = 0.25f;
                }
                else if (vert < 0 && currentSelection < thingsToBuy.Count - 1)
                {
                    currentSelection++;
                    moved = true;
                    timeBetweenSelections = 0.25f;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    TryToBuy(thingsToBuy[currentSelection]);
                    timeBetweenSelections = 0.25f;
                }

                if (moved)
                {
                    Vector3 ap = arrow.transform.position;
                    RectTransform art = arrow.transform as RectTransform;
                    RectTransform brt = thingsToBuy[currentSelection].gameObject.transform as RectTransform;

                    arrow.transform.position = brt.position;
                    arrow.transform.SetPosition(x: brt.position.x - brt.sizeDelta.x * 0.5f - art.sizeDelta.x * 0.5f);
                }
            }

            if (timeBetweenSelections > 0)
                timeBetweenSelections -= Time.deltaTime;

        }
    }

    public void TryToBuy(ThingToBuy toBuy)
    {
        if (toBuy.myUpgrade == ShopUpgrade.Exit)
            Leave();
        else
        {
            toBuy.TryToBuy();
            foreach (ThingToBuy ttb in thingsToBuy)
                ttb.SetLevel();
        }
    }

    public void Leave()
    {
        hasControl = false;
        shopWindow.SetActive(false);
        StartCoroutine(MoveTransformToPositionAtSpeed(fakeShip.transform, shipStart.position, 200, () => {
            StartCoroutine(WorldManager.Instance.FadeScreen(false, () =>
            {
                SceneManager.LoadScene(3);
            }));
        }));
    }

    public void ShowShop()
    {
        hasControl = true;
        shopWindow.SetActive(true);
        foreach (ThingToBuy ttb in thingsToBuy)
            ttb.SetLevel();
    }

    private IEnumerator MoveTransformToPositionAtSpeed(Transform toMove, Vector2 moveToPosition, float moveSpeed, Action endAction)
    {
        Vector2 moveDirection = moveToPosition - (Vector2)toMove.position;
        float totalDistance = moveDirection.magnitude;
        float currentMoveDistance = 0f;
        float totalTimeTest = 0f;
        while (currentMoveDistance < totalDistance)
        {
            Vector2 movePerUpdate = moveDirection.normalized * Time.deltaTime * moveSpeed;
            currentMoveDistance += movePerUpdate.magnitude;
            totalTimeTest += Time.deltaTime;

            if (currentMoveDistance >= totalDistance)
            {
                toMove.transform.SetPosition(moveToPosition.x, moveToPosition.y);// .position = moveToPosition;
                yield return new WaitForSeconds(0.5f);
                endAction?.Invoke();
                yield break;
            }
            else
            {
                toMove.transform.position += new Vector3(movePerUpdate.x, movePerUpdate.y, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
