using System;

public class Sceen{
    public void Lobby(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine($"[User] {user.GetUserName()} Lv:{user.GetStats().GetLevel()}");
        user.GetCurrency().Print_Currency();
        Console.WriteLine("{Profile} {Quest} {Dungeon} {Shop} {Community} {Chat} {Logout} {exit}");
        master.MenuSelectionEvent(user,master);
    }

    public void Community(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Community ]");
        master.GetDataService().ShowPost();
        Console.WriteLine("{Post} {Back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "Post":
                    Select = true;
                    master.CreatePost(user,master);
                    break;
                case "Back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
        
    }
}