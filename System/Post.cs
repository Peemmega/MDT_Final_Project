public class Post{
    private User _creator;
    private List<User> _liked = new List<User>();
    private string _file = "-";
    private string _description;
    private List<string> _comments = new List<string>();


    // Text Post
    public Post(User user,string text){
        _creator = user;
        _description = text;
    }
    // Image & Video Post
    public Post(User user,string text,string file){
        _creator = user;
        _description = text;
        _file = file;
    }
    public User GetUser(){
        return _creator;
    }

    public string GetDescription(){
        return _description;
    }
    public int GetLikeCount(){
        return _liked.Count;
    }
    public string GetFile(){
        return _file;
    }

    public List<string> GetComment(){
        return _comments;
    }
    public void Comment(User user, string text){
        _comments.Add($"[{user.GetUserName()}] {text}");
    }

    public void LikePost(User user){
        if (_liked.Contains(user)){
            _liked.Remove(user); 
        } else {
            _liked.Add(user); 
        }
    }

    
}



