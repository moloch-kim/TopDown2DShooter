using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    // 기본스텟과 추가스텟을 계산해 최종 스텟을 계산하는 로직
    // 근데 지금은 기본만

    [SerializeField] private CharacterStat baseStat;
    public CharacterStat CurrentStat { get; private set; }
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if (baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }

        CurrentStat = new CharacterStat { attackSO = attackSO };
        // TODO : 지금은 기본 능력치만, 앞으로는 능력치강화기능 적용
        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}
