using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class SwipeManager : MonoBehaviour
{

  public float swipeThreshold = 50f;
  public float timeThreshold = 0.3f;
  public float swipeCooldown = 0.1f; // Delay between swipes

  public UnityEvent OnSwipeLeft;
  public UnityEvent OnSwipeRight;
  public UnityEvent OnSwipeUp;
  public UnityEvent OnSwipeDown;

  private Vector2 fingerDown;
  private DateTime fingerDownTime;
  private Vector2 fingerUp;
  private DateTime fingerUpTime;

  private float lastSwipeTime = -Mathf.Infinity;

  private void Update()
  {
    if (Time.time - lastSwipeTime < swipeCooldown) return;

    if (Input.GetMouseButtonDown(0))
    {
      fingerDown = Input.mousePosition;
      fingerUp = Input.mousePosition;
      fingerDownTime = DateTime.Now;
    }

    if (Input.GetMouseButtonUp(0))
    {
      fingerDown = Input.mousePosition;
      fingerUpTime = DateTime.Now;
      CheckSwipe();
    }

    foreach (Touch touch in Input.touches)
    {
      if (touch.phase == TouchPhase.Began)
      {
        fingerDown = touch.position;
        fingerUp = touch.position;
        fingerDownTime = DateTime.Now;
      }

      if (touch.phase == TouchPhase.Ended)
      {
        fingerDown = touch.position;
        fingerUpTime = DateTime.Now;
        CheckSwipe();
      }
    }
  }

  private void CheckSwipe()
  {
    float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;
    if (duration > timeThreshold) return;

    float deltaX = fingerDown.x - fingerUp.x;
    float deltaY = fingerDown.y - fingerUp.y;

    if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
    {
      // Swipe horizontal
      if (Mathf.Abs(deltaX) > swipeThreshold)
      {
        if (deltaX > 0)
          OnSwipeRight.Invoke();  // Swiped left
        else
          OnSwipeLeft.Invoke(); // Swiped right

        lastSwipeTime = Time.time;
      }
    }
    else
    {
      // Swipe vertical
      if (Mathf.Abs(deltaY) > swipeThreshold)
      {
        if (deltaY > 0)
          OnSwipeUp.Invoke(); // Swiped down
        else
          OnSwipeDown.Invoke();   // Swiped up

        lastSwipeTime = Time.time;
      }
    }

    fingerUp = fingerDown;
  }
}