public class Currency{
    private int _Gold = 0;
    private int _Gem = 0;

    public void Add(string currency,int value){
        if (currency == "Gold"){
            Console.WriteLine($"[Gold] {_Gold} -> {_Gold + value}");
            _Gold += value;
        } else if (currency == "Gem"){
            Console.WriteLine($"[Gem] {_Gem} -> {_Gem + value}");
            _Gem += value;
        }
    }

    public void Remove(string currency,int value){
        if (currency == "Gold"){
            _Gold -= value;
        } else if (currency == "Gem"){
            _Gem -= value;
        }
    }

    public void Print_Currency(){
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"[Gold] : {_Gold} [Gem] : {_Gem}");
    }
}