using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data", order = 0)]
public class CharacterData : ScriptableObject
{
    public Sprite Icon;
    public Sprite FullBody;
    public string CharacterName;
}
