using MedHistoryTrackerV2.Data.Models;

namespace MedHistoryTrackerV2.Repository.Abstractions;

public interface IMedicineRepository
{
    bool AddMedicine(MedicineModel medicine);
    MedicineModel UpdateMedicine(MedicineModel medicine);
    MedicineModel DeleteMedicine(int? medicineId);
    IList<MedicineModel> GetAllMedicines();
    MedicineModel GetMedicineById(int? medicineId);
}
