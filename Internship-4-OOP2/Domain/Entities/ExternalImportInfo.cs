namespace Internship_4_OOP2.Domain.Entities
{
    public class ExternalImportInfo
    {
        public int Id { get; private set; }
        public DateTime LastImportedAt { get; private set; }

        private ExternalImportInfo() { }

        public ExternalImportInfo(DateTime lastImportedAt)
        {
            LastImportedAt = lastImportedAt;
        }

        public bool IsCacheValid()
        {
            return LastImportedAt.Date == DateTime.UtcNow.Date;
        }

        public void RefreshTimestamp()
        {
            LastImportedAt = DateTime.UtcNow;
        }
    }
}
