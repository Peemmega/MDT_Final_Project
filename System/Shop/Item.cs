public class Item {
    private string _name;
    private int _cost;

    public Item (string name,int cost){
        _name = name;
        _cost = cost;
    }
    public string GetName(){
        return _name;
    }
    public int GetCost(){
        return _cost;
    }

    public void Use(User user){
        Console.WriteLine($"[Used Item] {_name}");
            if (_name == "Potion"){
                user.RecoveryHP(10);
            } else if (_name == "Big Potion"){
                user.RecoveryHP(25);
            }
            user.Item_Inventory().Remove(this);
    }
    
}