public class Profile {
    private string _profile_Image = "Avatar.jpg";
    private string _profile_Banner = "Banner.jpg";
    private string _profile_Bio = "-";
    private NameSkin _NameSkin = new NameSkin("None","","",0);


    public string GetImage(){
        return _profile_Image;
    }
    public string GetBanner(){
        return _profile_Banner;
    }
    public string GetBio(){
        return _profile_Bio;
    }
    public NameSkin GetNameSkin(){
        return _NameSkin;
    }

    public void SetNameSkin(NameSkin skin){
        _NameSkin = skin;
    }

    public void Set_Bio(string bio){
        _profile_Bio = bio;
    }
}
