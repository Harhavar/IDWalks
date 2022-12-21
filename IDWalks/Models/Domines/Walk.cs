namespace IDWalks.Models.Domines
{
    public class Walk
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDeficultyId { get; set; }

        public Region region { get; set; }

        public WalkDeficulty walkDeficulty { get; set; }
    }
}
