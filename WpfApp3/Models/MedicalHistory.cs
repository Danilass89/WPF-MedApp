namespace WpfApp3.Models
{
    internal class MedicalHistory
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
        public string Date { get; set; }
        public string Diagnosis { get; set; }
        public string EndDate { get; set; }
    }
}
