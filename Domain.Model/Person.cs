namespace Domain.Model
{
    public class Person
    {
        public Person(string name, int bornIn)
        {
            this.Name = name;
            this.BornIn = bornIn;
        }

        public string Name { get; set; }

        public int BornIn { get; set; }
    }
}