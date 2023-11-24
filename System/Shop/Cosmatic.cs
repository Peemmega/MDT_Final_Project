public class NameSkin {
    private string _name;
    private string _front;
    private string _back;
    private int _cost;

    public NameSkin (string name,string front,string back,int cost){
        _name = name;
        _front = front;
        _back = back;
        _cost = cost;
    }
    public string GetName(){
        return _name;
    }
    public int GetCost(){
        return _cost;
    }
    public string GetFrontText(){
        return _front;
    }
    public string GetBackText(){
        return _back;
    }
    public void Use(User user){
        if (user.GetProfile().GetNameSkin().GetName() != _name){
            user.GetProfile().SetNameSkin(this);
            Console.WriteLine($"[Change Name Skin to {_name}]");
        } else {
            user.GetProfile().SetNameSkin(new NameSkin("None","","",0));
            Console.WriteLine($"[Change Name Skin to None]");
        }
    }
}