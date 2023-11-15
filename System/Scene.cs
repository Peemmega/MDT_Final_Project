using System;

public class Sceen{
    public void Lobby(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine($"[User] {user.GetUserName()} Lv:{user.GetStats().GetLevel()}");
        user.GetCurrency().Print_Currency();
        Console.WriteLine("{Profile} {Quest} {Dungeon} {Shop} {Cummonity} {Chat} {Logout}");
        master.MenuSelectionEvent(user,master);
    }
}