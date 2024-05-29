using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  public GameObject endGameUI;
  public void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Finish"))
    {
      endGameUI.SetActive(true);
      Time.timeScale = 0;
    }
  }
}
