namespace Ugugushka.Domain.DtoModels
{
    public class PartitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PartitionDto part)
                return part.Id == Id;

            return base.Equals(obj);
        }

        public override int GetHashCode() => 
            Id.GetHashCode();
    }
}
