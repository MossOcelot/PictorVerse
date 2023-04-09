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
        Mail newMails = new Mail("Dead", $"แจ้งเตือนการเสียชีวิต วันที่ {date[0]}/{date[1]}/{date[2]}",
                      $"คุณ {player_status.getPlayerName()} ได้เสียชีวิตแล้ว เมื่อเวลา {date[3]} ชั่วโมง {date[4]} นาที หากคุณมีประกัน ประกันได้ถูกใช้ไปแล้ว โปรดซื้อประกันอีกครั้งก่อนทำอะไร เพื่อความปลอดภัย");
        mail_manager.AddMails(newMails);
    }


}
