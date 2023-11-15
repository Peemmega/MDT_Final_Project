public class Data{
    private Dictionary<string,User> _users_Data = new Dictionary<string,User>();
    private Dictionary<string,Post> _post_Data = new Dictionary<string,Post>();
    
    public int GetUserCount(){
       return _users_Data.Count;
    }
    public int GetPostCount(){
       return _post_Data.Count;
    }

    public Data(){
        // Admin Account for test
        AddUserData("Peemmega",new User("Peemmega","12345678"));
    }

    // Add Data
    public void AddUserData(string username,User data){
        _users_Data.Add(username,data);
    }
    public void AddPostData(string postID,Post data){
        _post_Data.Add(postID,data);
    }

    public void ShowPost(){
        Console.WriteLine("-----------------------------------------------------------");
        foreach (KeyValuePair<string,Post> post in _post_Data){
            Console.WriteLine($"[Post ID:{post.Key}]");
            Console.WriteLine($"{post.Value.GetUser().GetUserName()}");
            Console.WriteLine($"- {post.Value.GetDescription()}");

            if (post.Value.GetFile() != "-"){
                Console.WriteLine($"[File] {post.Value.GetFile()}");
            }
            Console.WriteLine($"[Like : {post.Value.GetLikeCount()}]");
            Console.WriteLine("-----------------------------------------------------------");
        }
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