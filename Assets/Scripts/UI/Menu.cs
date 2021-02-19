using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class Menu : MonoBehaviour
    {
        public void OpenClose()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}