public class Post{
    private User _creator;
    private int _like = 0;
    private string _file = "-";
    private string _description;
    
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
}