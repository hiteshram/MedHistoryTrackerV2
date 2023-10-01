using MedHistoryTrackerV2.Data.Models;
using MedHistoryTrackerV2.Repository.Abstractions;
using System.Data.OleDb;
using Dapper;

namespace MedHistoryTrackerV2.Repository;

public class MedicineRepositoryAccess : IMedicineRepository
{
    public string connString { get; set; }

    public MedicineRepositoryAccess(string connString)
    {
        this.connString = connString;
    }

    bool IMedicineRepository.AddMedicine(MedicineModel medicine)
    {
        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            string queryText = "INSERT INTO Medicine (Name,Combination) Values (@Name,@Combination)";
            int data = conn.Execute(queryText, new { Name = medicine.Name, Combination = medicine.Combination });
            return true;
        }
    }

    MedicineModel IMedicineRepository.DeleteMedicine(int? medicineId)
    {
        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            string queryText = "DELETE FROM Medicine WHERE Id=@Id";
            int data = conn.Execute(queryText, new { Id = medicineId });
            return new MedicineModel();
        }
    }

    IList<MedicineModel> IMedicineRepository.GetAllMedicines()
    {
        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            string queryText = "SELECT * FROM Medicine";
            IEnumerable<MedicineModel> data = conn.Query<MedicineModel>(queryText);
            return data.ToList();
        }
    }

    MedicineModel IMedicineRepository.GetMedicineById(int? medicineId)
    {
        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            string queryText = "SELECT * FROM Medicine where Id=@Id";
            MedicineModel data = conn.Query<MedicineModel>(queryText, new { Id = medicineId }).First();
            return data;
        }
    }

    MedicineModel IMedicineRepository.UpdateMedicine(MedicineModel medicine)
    {
        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            string queryText = "UPDATE Medicine SET Name=@Name, Combination=@Combination WHERE Id=@Id";
            int data = conn.Execute(queryText, new { Name = medicine.Name, Combination = medicine.Combination, Id = medicine.Id });
            return new MedicineModel();
        }
    }
}
