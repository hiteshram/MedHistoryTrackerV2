using MedHistoryTrackerV2.Data.Models;
using MedHistoryTrackerV2.Repository.Abstractions;

namespace MedHistoryTrackerV2.Services;

public class MedicineService
{
    public IMedicineRepository MedicineRepository { get; private set; }

    public MedicineService(IMedicineRepository repository) => MedicineRepository = repository;

    public bool AddMedicine(MedicineModel medicine)
    {
        MedicineRepository.AddMedicine(medicine);
        return true;
    }

    public MedicineModel DeleteMedicine(int? medicineId)
    {
        return MedicineRepository.DeleteMedicine(medicineId);
    }

    public IList<MedicineModel> GetAllMedicines()
    {
        return MedicineRepository.GetAllMedicines();
    }

    public MedicineModel GetMedicineById(int? medicineId)
    {
        return MedicineRepository.GetMedicineById(medicineId);
    }

    public MedicineModel UpdateMedicine(MedicineModel medicine)
    {
        return MedicineRepository.UpdateMedicine(medicine);
    }

    public IDictionary<int, string> GetMedicineDictionary()
    {
        IDictionary<int, string> medicineDictionary = new Dictionary<int, string>();
        IList<MedicineModel> Medicines = MedicineRepository.GetAllMedicines();

        foreach (var medicine in Medicines)
        {
            medicineDictionary[medicine.Id] = medicine.Name;
        }
        return medicineDictionary;
    }
}
