namespace web2.Models
{
    public class Report
    {
        public long RowID = 0;
        public long UID = 0;
        public long IDToReport = 0;
        public bool Resolved = false;

        public enum ProblemTypes 
        {  
            NoType = 0,
            MisleadingorScam = 1,
            SexuallyInappropriate = 2,
            Offensive = 3,
            Violent = 4,
            Spam = 5
        }

    }
}