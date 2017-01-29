namespace Data.Queries
{
    using System;

    public abstract class BaseQuery : IQuery
    {
        public BaseQuery(int offset)
        {
            this.OffSet = offset;
        }

        public int OffSet { get; set; }

        public string Query { get; set; }

        public virtual void Validate()
        {
            if (this.OffSet < 0)
            {
                throw new ArgumentException("Invalid OffSet Value. Must be superior to 0.");
            }
        }

        public abstract void Build();
    }
}