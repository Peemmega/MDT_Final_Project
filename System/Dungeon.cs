public class Reward {
    private int _exp;
    private int _gold;

    public Reward(int exp,int gold){
        _exp = exp;
        _gold = gold;
    }

    public int GetGold(){
        return _gold;
    }
    public int GetEXP(){
        return _exp;
    }
}

public class Stage {
    private string _name;
    private string _recomment = "";
    private Reward _reward;
    public List<Monster> _monsterOnStage;
    
    public Stage(string name, List<Monster> monsters,Reward reward){
        _name = name;
        _monsterOnStage = monsters;
        _reward = reward;
    }

    public Stage(string name, List<Monster> monsters,Reward reward,string recomment){
        _name = name;
        _monsterOnStage = monsters;
        _reward = reward;
        _recomment = recomment;
    }
    
    public string GetName(){
        return _name;
    }
    public string GetRecomment(){
        return _recomment;
    }
    public List<Monster> GetMonster(){
        return _monsterOnStage;
    }
    public int GetMonsterCount(){
        return _monsterOnStage.Count;
    }
     public Reward GetReward(){
        return _reward;
    }
    public void ShowEnemyList(){
        foreach (Monster monster in _monsterOnStage){
            if (monster.GetHP() > 0){
                Console.Write($"[{monster.GetName()} HP:{monster.GetHP()}] ");
            } else {
                Console.Write($"[ - ] ");
            }
            
        }
    }
}

