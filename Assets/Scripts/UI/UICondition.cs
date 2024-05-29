using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    public GameObject player;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
    private void Update()
    {
        transform.position = player.transform.position + Vector3.up; // Condition유아이가 Player캐릭터를 따라다니도록 설정
    }
}
