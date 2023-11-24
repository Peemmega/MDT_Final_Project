public class User{
    private string _username;
    private string _nickname;
    private string _password;
    private int _hp;
    private int _blockValue = 0;
    private Currency _currency = new Currency();
    private Stats _stats;
    private Profile _profile;
    private List<Item> _Item_Inventory = new List<Item>();
    private Dictionary<string,NameSkin> _MyNameSkin = new Dictionary<string,NameSkin>();
    private List<User> _follow = new List<User>();
    private List<User> _follower = new List<User>();

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

    public List<User> GetFollow(){
        return _follow;
    }
    public List<User> GetFollower(){
        return _follower;
    }

    public string GetUserNameSkin(){
        NameSkin skin = _profile.GetNameSkin();
        return $"{skin.GetFrontText()}{_username}{skin.GetBackText()}";
    }

    public string GetPassWord(){    
        return _password;
    }
    public Stats GetStats(){
        return _stats;
    }
    public Profile GetProfile(){
        return _profile;
    }
    public int GetHP(){
        return _hp;
    }
    public void RecoveryHP(int val){
        _hp = Math.Clamp(_hp + val,_hp,_stats.GetHP());
        Console.WriteLine($"[Recover] HP to {_hp}");
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

    public List<Item> Item_Inventory(){
        return _Item_Inventory;
    }
    public Dictionary<string,NameSkin> NameSkin_Inventory(){
        return _MyNameSkin;
    }
    public void ShowItemList(){
        int i = 0;
        foreach (Item item in _Item_Inventory){
            Console.WriteLine($"[{i}] {item.GetName()} ");
            i++;
        }
    }
    public void ShowNameSkinList(){
        foreach (KeyValuePair<string,NameSkin> skin in _MyNameSkin){
            Console.WriteLine($"[ {skin.Value.GetName()} ]");
        }
    }   
    public void ChangeUserName(Gamemaster master){
        Console.Write("New username: ");
        string user_name = master.GetString(1,20);
        Console.WriteLine("Confirm new username?????: [y/n]");

        switch (Console.ReadLine()){
             case "y" :
                master.Print("Change username to " + user_name);
                _username = user_name;
                break;
            default: 
                master.Print("Cancel name change");
                break;       
         }
    }

    public void ChangeBio(Gamemaster master){
        Console.Write("New Bio: ");
        string newBio = master.GetString(1,20);
        Console.WriteLine("Confirm your new bio?????: [y/n]");

        switch (Console.ReadLine()){
             case "y" :
                master.Print("Change bio to: " + newBio);
                _profile.Set_Bio(newBio);
                break;
            default: 
                master.Print("Cancel bio change");
                break;       
         }
    }

    
}