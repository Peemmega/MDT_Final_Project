public class User{
    private string _username;
    private string _nickname;
    private string _password;
    private int _userID;
    private Currency _currency;
    private Stats _stats;
    private Profile _profile;


    public User(string userName,string password){
        _username = userName;
        _nickname = userName;
        _password = password;
        _stats = new Stats();
        _profile = new Profile();
    }
    public string GetUserName(){
        return _username;
    }
    public string GetPassWord(){
        return _password;
    }
    public Stats GetStats(){
        return _stats;
    }
    public int GetUserID(){
        return _userID;
    }
    public void ChangeUserName(){
        Gamemaster master = new Gamemaster();
        Console.Write("New Username: ");
        string user_name = master.GetString(1,20);
        Console.WriteLine("Confirm your account?????: [Y/N]");

        switch (Console.ReadLine()){
             case "Y" :
                master.Print("Change Username to " + user_name);
                _username = user_name;
                break;
            default: 
                master.Print("Cancel name change");
                break;       
         }
    }

    
}