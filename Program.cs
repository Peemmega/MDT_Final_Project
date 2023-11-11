public class Enemy_Status{
    private int hp;
    private int attack;
    private int defend;
    private int speed;
}

public class User{
    private string _username;
    private string _password;
    private int _lvl = 1;
    private int _exp = 0;


    public User(string userName,string password){
        _username = userName;
        _password = password;
    }
    public string GetUserName(){
        return _username;
    }

}

class Program {
    static void Main(string[] args){
        Sceen appScene = new Sceen();
        User user = appScene.Login();
        appScene.Lobby(user);
    }
}