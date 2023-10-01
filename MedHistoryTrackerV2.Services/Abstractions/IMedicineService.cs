using MedHistoryTrackerV2.Data.Models;
using MedHistoryTrackerV2.Repository.Abstractions;
namespace MedHistoryTrackerV2.Services.Abstractions;

public interface IMedicineService
{
    IMedicineRepository MedicineRepository { get; }
    bool AddMedicine(MedicineModel medicine);
    MedicineModel UpdateMedicine(MedicineModel medicine);
    MedicineModel DeleteMedicine(int? medicineId);
    IList<MedicineModel> GetAllMedicines();
    MedicineModel GetMedicineById(int? medicineId);
    IDictionary<int, string> GetMedicineDictionary();
}
