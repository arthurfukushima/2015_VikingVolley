using UnityEngine;
using System.Collections;

public static class StateMessages
{
    // this ints could be an enumerator instead of fixed values..
    // > 1000 means UI messages
    public const int UI_MainMenu_OnPlayClick = 1001;
    public const int UI_MainMenu_OnClose = 1002;



    // > 4000 is gameplay messaes
    public const int GP_OnGameOver = 4001;

}
