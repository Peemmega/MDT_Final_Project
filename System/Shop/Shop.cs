public class Shop{
    private Dictionary<string,Item> _Items = new Dictionary<string,Item>();
    private Dictionary<string,NameSkin> _NameSkin = new Dictionary<string,NameSkin>();
    
    public Shop(){
        _NameSkin.Add("Summer", new NameSkin("Summer","[S_","_S]",100));
        _NameSkin.Add("Winter", new NameSkin("Winter","[W_","_W]",100));
        _NameSkin.Add("Yinyang", new NameSkin("Yinyang","[6_","_9]",100));
        
        _Items.Add("Potion", new Item("Potion",20));
        _Items.Add("Big Potion",new Item("Big Potion",50));
        _Items.Add("Super Potion",new Item("Super Potion",200));
    }

    public void ShowItemList(){
        Console.WriteLine("--------------------- [ Items ] -----------------------");

        foreach (KeyValuePair<string,Item> item in _Items){
            Console.WriteLine($"[Item: {item.Value.GetName()} {item.Value.GetCost()} Gold]");
        }
    }
    public void ShowCosmaticList(User user){
        Console.WriteLine("--------------------- [ Cosmatics ] -----------------------");

        foreach (KeyValuePair<string,NameSkin> skin in _NameSkin){
            Console.WriteLine($"[Item: {skin.Value.GetName()} {skin.Value.GetCost()} Gold]");
            Console.WriteLine($"   Priview: {skin.Value.GetFrontText()}{user.GetUserName()}{skin.Value.GetBackText()}");
        }
    }

    public void Buy(User user, Gamemaster master, string boughtname){
        if (_Items.ContainsKey(boughtname)){
            if (_Items[boughtname].GetCost() <= user.GetCurrency().GetGold()){
                Console.WriteLine($"[Success] bought {boughtname}");
                user.GetCurrency().Remove("Gold", _Items[boughtname].GetCost());
                master.AddItem(user,_Items[boughtname]);
            } else {
                Console.WriteLine($"[Fail] Gold not enough");
            }
        } else if (_NameSkin.ContainsKey(boughtname)){
            if (_NameSkin[boughtname].GetCost() <= user.GetCurrency().GetGold()){
                if (!user.NameSkin_Inventory().ContainsKey(boughtname)){
                    Console.WriteLine($"[Success] bought {boughtname}");
                    user.GetCurrency().Remove("Gold", _NameSkin[boughtname].GetCost());
                    master.AddNameSkin(user,_NameSkin[boughtname]);
                } else {
                    Console.WriteLine($"[Fail] You already have {boughtname} in your inventory");
                }
                
            } else {
                Console.WriteLine($"[Fail] Gold not enough");
            }
        } else {
            Console.WriteLine($"[Fail] {boughtname} not in shop list");
        }
    }
        
    
}