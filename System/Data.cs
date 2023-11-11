public class Data{
    private Dictionary<string,User> _users_Data = new Dictionary<string,User>();
    
    public void AddUserData(string username,User data){
        _users_Data.Add(username,data);
    }
    public int GetUserCount(){
       return _users_Data.Count;
    }

    public void ShowList(){
        Console.WriteLine("-----------------------------------------------------------");
        foreach (KeyValuePair<string,User> user in _users_Data){
            Console.WriteLine($"[ID:{user.Key}] {user.Value.GetUserName()}");
        }
    }

    public string GetPassWord(){
        return "A";
    }

    public bool IsUsernameInData(string username){
        return  _users_Data.ContainsKey(username);
    }

    public User GetUserFormUserName(string username){
        User val = _users_Data[username];
        return val;
    }

}