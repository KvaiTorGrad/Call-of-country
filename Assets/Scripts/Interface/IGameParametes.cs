interface IGameParametes
{
    public float evolution { set; get; }
    public float money { set; get; }
    public float progress { set; get; }

    public void ProgressSystem();
}
interface IButtonParametrs
{
    public int costAttack_1 { set; get; }
    public int costAttack_2 { set; get; }
    public int costAttack_3 { set; get; }
    public int costEconomy_1 { set; get; }
    public int costEconomy_2 { set; get; }
    public int costEconomy_3 { set; get; }
    public int costDevelopment_1 { set; get; }
    public int costDevelopment_2 { set; get; }
    public int costDevelopment_3 { set; get; }

   public void Attack_1();
    public void Attack_2();
    public void Attack_3();

    public void Economy_1();
    public void Economy_2();
    public void Economy_3();

    public void Development_1();
    public void Development_2();
    public void Development_3();

}
