using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] public bool isTrapActive = false;

    [SerializeField] public enum type
    {
        Mirror,Toilet,Hangman
    }
    public type _type;




}
