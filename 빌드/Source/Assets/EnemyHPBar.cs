﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MC.UI
{
    public class EnemyHPBar : MonoBehaviour
    {
        public HPBar hpBar;
        public Text name;
        public Image[] wings;

        public void HpBarView()
        {
            if(GameStatus.currentGameState == CurrentGameState.MissionClear ||
                GameStatus.currentGameState == CurrentGameState.End)
            {
                hpBar.gameObject.SetActive(false);
                name.gameObject.SetActive(false);
                wings[0].gameObject.SetActive(false);
                wings[1].gameObject.SetActive(false);
                return;
            }

            if (PlayerFSMManager.Instance.LastHit() == null)
            {
                hpBar.gameObject.SetActive(false);
                name.gameObject.SetActive(false);
                wings[0].gameObject.SetActive(false);
                wings[1].gameObject.SetActive(false);
            }
            else
            {
                hpBar.gameObject.SetActive(true);
                name.gameObject.SetActive(true);
                wings[0].gameObject.SetActive(true);
                wings[1].gameObject.SetActive(true);
                UserInterface.Instance.HPChangeEffect(PlayerFSMManager.Instance.LastHit(), hpBar);
                name.text = SetName(PlayerFSMManager.Instance.LastHit());
            }
        }

        public void SetFalse()
        {
            if (GameStatus.currentGameState == CurrentGameState.MissionClear ||
                   GameStatus.currentGameState == CurrentGameState.End)
            {
                hpBar.gameObject.SetActive(false);
                name.gameObject.SetActive(false);
                wings[0].gameObject.SetActive(false);
                wings[1].gameObject.SetActive(false);
                return;
            }

            if (!GameStatus.GameClear)
            {
                if (PlayerFSMManager.Instance != null)
                {
                    PlayerFSMManager.Instance.Stat.lastHitBy = null;
                }
                hpBar.gameObject.SetActive(false);
                name.gameObject.SetActive(false);
                wings[0].gameObject.SetActive(false);
                wings[1].gameObject.SetActive(false);
            }
        }

        string SetName(CharacterStat stat)
        {
            switch (stat.monsterType)
            {
                case MonsterType.Mac:
                    return "맥";
                case MonsterType.RedHat:
                    return "레드 햇";
                case MonsterType.Tiber:
                    return "티버";
                case MonsterType.Length:
                    return "리리스";
                default:
                    return "AA";
            }
        }

        public void AttackSupport()
        {
            hpBar.HitBackFun();
        }



    }
}