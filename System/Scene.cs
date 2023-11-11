using System;

public class Sceen{
    public void Lobby(User user){
        Gamemaster master = new Gamemaster();
        master.CreateLine();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine($"[User] {user.GetUserName()} Lv:{user.GetStats().GetLevel()}");
        Console.WriteLine("[1] Yo");
        Console.WriteLine("[2] Yi");
        Console.WriteLine("[3] Ya");
    }

    public User Login(){
          
        static User Get_User(Gamemaster master){
            master.PrintNL("[ Login ]");
            master.Print("Account: ");
            string user_name = master.GetString(1,20);
            master.Print("Password: ");
            string user_password = master.GetString(8,24);
            User your_Account = new User(user_name,user_password);

            bool confirm = master.YesNo();

            switch (confirm){
                case false :
                    your_Account = Get_User(master);
                    break; 
            }

            return your_Account;
        }

        Gamemaster master = new Gamemaster();
        User your_account = Get_User(master);
        Console.Clear();
        master.CreateLine();
        master.PrintNL("Login success!");
        return your_account;
    }

}