public class Stats{
    private int _level = 1;
    private int _exp = 0;
    private int _hp = 10;
    private int _attack = 2;
    private int _defend = 1;
    private int _speed = 3;

    public int GetLevel(){
        return _level;
    }
    public int GetEXP(){
        return _exp;
    }
    public int GetHP(){
        return (int) (_hp  *  (1 + ((_level-1) * 0.5)));
    }
    public int GetATK(){
        return (int) (_attack  *  (1 + ((_level-1) * 0.5)));
    }
    public int GetDEF(){
        return (int) (_defend  *  (1 + ((_level-1) * 0.5)));
    }
    public int GetSPD(){
        return (int) (_speed  *  (1 + ((_level-1) * 0.5)));
    }

    public Stats(){
        // Create Player Stats
    }

    public Stats(int hp,int atk,int def, int spd){
        _hp = hp;
        _attack = atk;
        _defend = def;
        _speed = spd;
    }

    public void SetLevel(int val){
        _level = val;
    }
    public void LevelUp(){
        Console.WriteLine($"[ Level up ] {_level} to {_level + 1}");
        _exp -= _level * 250;
        ++_level;
    }

    public void AddEXP(int val){
        Console.WriteLine($"[ Exp ] {_exp}/{_level * 250} -> {_exp + val}/{_level * 250}");
        _exp += val;
        while (_exp >= (_level * 250)) {
            LevelUp();
        }
    }   

    public void ShowStats (){
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine($"[Stats] lv: {_level} exp: {_exp}/{_level * 250}");
        Console.WriteLine($"[HP] {GetHP()}");
        Console.WriteLine($"[ATK] {GetATK()}");
        Console.WriteLine($"[DEF] {GetDEF()}");
        Console.WriteLine($"[SPD] {GetSPD()}");
    }
}
