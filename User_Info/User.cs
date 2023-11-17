public class User{
    private string _username;
    private string _nickname;
    private string _password;
    private Currency _currency = new Currency();
    private Stats _stats;
    private Profile _profile;
    private int _hp;
    private int _blockValue = 0;

    public User(string userName,string password){
        _username = userName;
        _nickname = userName;
        _password = password;
        _stats = new Stats();
        _profile = new Profile();
        _hp = _stats.GetHP();
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
    public int GetHP(){
        return _hp;
    }
    public int GetBlockValue(){
        return _blockValue;
    }

    public void RecoveryToMaxHP(){
        _hp = _stats.GetHP();
        _blockValue = 0;
    }
    public void TakeDamage(int val){
        _hp -= val;
    }
    
    public void UseBlock(){
        _blockValue += 2;
    }
    public void ReduceBlockValue(){
        if (_blockValue > 0){
            _blockValue -= 1;
        }   
    }
    public Currency GetCurrency(){
        return _currency;
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