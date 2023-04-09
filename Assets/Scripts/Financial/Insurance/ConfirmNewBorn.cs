using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmNewBorn : MonoBehaviour
{
    public GameObject CloneObj;
    public void NewGame()
    {
        PlayerStatus player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        UIGameOverController uiGameOver = GameObject.FindGameObjectWithTag("GameOver").gameObject.GetComponent<UIGameOverController>();

        Timesystem time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.AddComponent<Timesystem>();
        int[] date = time_system.getDateTime();
        player_status.newBorn();
        Destroy(CloneObj);
        uiGameOver.Close();

        MailManager mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<MailManager>();
        Mail newMails = new Mail("Dead", $"����͹������ª��Ե �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                      $"�س {player_status.getPlayerName()} �����ª��Ե���� ��������� {date[3]} ������� {date[4]} �ҷ� �ҡ�س�ջ�Сѹ ��Сѹ��١������� �ô���ͻ�Сѹ�ա���駡�͹������ ���ͤ�����ʹ���");
        mail_manager.AddMails(newMails);
    }


}
